using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Dapper;

public static class DapperExtensions
{
    public static void Insert(this IDbConnection connection, object entityToInsert)
    {
        Insert(connection, null, entityToInsert);
    }

    public static void Insert(this IDbConnection connection, string sql, object entityToInsert)
    {
        var name = entityToInsert.GetType().Name;
        var sb = new StringBuilder(sql);
        if (sql == null)
            sb.AppendFormat("insert into {0}", name);
        sb.Append(" (");
        BuildInsertParameters(entityToInsert, sb);
        sb.Append(") values (");
        BuildInsertValues(entityToInsert, sb);
        sb.Append(")");
        connection.Execute(sb.ToString(), entityToInsert);
    }

    public static int Update(this IDbConnection connection, object entityToUpdate)
    {
        return Update(connection, null, entityToUpdate);
    }

    public static int Update(this IDbConnection connection, string sql, object entityToUpdate)
    {
        var idProps = GetIdProperties(entityToUpdate);
        if (idProps.Count() == 0)
            throw new ArgumentException("Entity must have at least one [Key] property");

        var name = entityToUpdate.GetType().Name;

        var sb = new StringBuilder(sql);
        if (sql == null)
            sb.AppendFormat("update {0}", name);

        sb.AppendFormat(" set ");
        BuildUpdateSet(entityToUpdate, sb);
        sb.Append(" where ");
        BuildWhere(sb, idProps.ToArray());

        return connection.Execute(sb.ToString(), entityToUpdate);
    }

    public static int Delete<T>(this IDbConnection connection, T entityToDelete)
    {
        return Delete(connection, null, entityToDelete);
    }

    public static int Delete<T>(this IDbConnection connection, string sql, T entityToDelete)
    {
        var idProps = typeof (T).IsAnonymousType() ? 
            GetAllProperties(entityToDelete) : 
            GetIdProperties(entityToDelete);

        if (idProps.Count() == 0)
            throw new ArgumentException("Entity must have at least one [Key] property");

        var name = entityToDelete.GetType().Name;

        var sb = new StringBuilder(sql);
        if (sql == null)
            sb.AppendFormat("delete from {0}", name);

        sb.Append(" where ");
        BuildWhere(sb, idProps);

        return connection.Execute(sb.ToString(), entityToDelete);
    }

    private static void BuildUpdateSet(object entityToUpdate, StringBuilder sb)
    {
        var nonIdProps = GetNonIdProperties(entityToUpdate).ToArray();

        for (var i = 0; i < nonIdProps.Length; i++)
        {
            var property = nonIdProps[i];

            sb.AppendFormat("{0} = @{1}", property.Name, property.Name);
            if (i < nonIdProps.Length - 1)
                sb.AppendFormat(", ");
        }
    }

    private static void BuildWhere(StringBuilder sb, IEnumerable<PropertyInfo> idProps)
    {
        for (var i = 0; i < idProps.Count(); i++)
        {
            sb.AppendFormat("{0} = @{1}", idProps.ElementAt(i).Name, idProps.ElementAt(i).Name);
            if (i < idProps.Count() - 1)
                sb.AppendFormat(" and ");
        }
    }

    private static void BuildInsertValues(object entityToInsert, StringBuilder sb)
    {
        var props = GetAllProperties(entityToInsert);

        for (var i = 0; i < props.Count(); i++)
        {
            var property = props.ElementAt(i);
            if (property.GetCustomAttributes(true).Where(a => a is KeyAttribute).Any()) continue;
            sb.AppendFormat("@{0}", property.Name);
            if (i < props.Count() - 1)
                sb.Append(", ");
        }
    }

    private static void BuildInsertParameters(object entityToInsert, StringBuilder sb)
    {
        var props = GetAllProperties(entityToInsert);

        for (var i = 0; i < props.Count(); i++)
        {
            var property = props.ElementAt(i);
            if (property.GetCustomAttributes(true).Where(a => a is KeyAttribute).Any()) continue;
            sb.Append(property.Name);
            if (i < props.Count() - 1)
                sb.Append(", ");
        }
    }

    private static IEnumerable<PropertyInfo> GetAllProperties(object entity)
    {
        return entity.GetType().GetProperties();
    }

    private static IEnumerable<PropertyInfo> GetNonIdProperties(object entity)
    {
        return GetAllProperties(entity).Where(p => p.GetCustomAttributes(true).Any(a => a is KeyAttribute) == false);
    }

    private static IEnumerable<PropertyInfo> GetScaffoldableProperties(object entity)
    {
        return GetNonIdProperties(entity).Where(p => p.GetCustomAttributes(true).Any(a => a is EditableAttribute) == false);
    }

    private static IEnumerable<PropertyInfo> GetIdProperties(object entity)
    {
        return GetAllProperties(entity).Where( p => p.GetCustomAttributes(true).Any(a => a is KeyAttribute));
    }
}

public static class TypeExtension
{
    public static Boolean IsAnonymousType(this Type type)
    {
        if (type == null) return false;

        var hasCompilerGeneratedAttribute = type.GetCustomAttributes(typeof(CompilerGeneratedAttribute), false).Count() > 0;
        var nameContainsAnonymousType = type.FullName.Contains("AnonymousType");
        var isAnonymousType = hasCompilerGeneratedAttribute && nameContainsAnonymousType;

        return isAnonymousType;
    }
}