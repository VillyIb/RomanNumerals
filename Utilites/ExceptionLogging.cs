using System;
using System.Collections;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Text;
using System.Web.Services.Protocols;
using System.Net;

using System.Data.Entity.Validation;
using System.Net.Mail;
using EU.Iamia.Logging;

// ReSharper disable SuggestBaseTypeForParameter

namespace nu.gtx.Common2.Utils
{
    public static class ExceptionLogging
    {
        private static void LogAny(ILog logger, Exception ex)
        {
            if (ex == null) { return; }

            foreach (DictionaryEntry current in ex.Data)
            {
                logger.ErrorFormat("    Data-Key: {0} = '{1}'", current.Key, current.Value);
            }

            logger.Error(ex.Message, ex);
        }


        #region Requires EntityFramework - move to other assembly
        private static void LogUtil(ILog logger, DbValidationError value)
        {
            if (value != null)
            {
                logger.ErrorFormat("DbValidationError-    {0},  Property: {1}", value.ErrorMessage, value.PropertyName);
            }
        }


        private static void LogUtil(ILog logger, DbEntityValidationResult value)
        {
            if (value != null)
            {
                logger.ErrorFormat(
                    "DbEntityValidationResult- IsValied: {0}, State: {1:G}"
                    , value.IsValid
                    , value.Entry.State
                );

                if (value.Entry.Entity != null)
                {
                    logger.ErrorFormat(
                        "- EntityName: {0}, {1}"
                        , value.Entry.Entity.GetType().Name
                        , value.Entry.Entity)
                    ;
                }

                foreach (var current in value.Entry.GetValidationResult().ValidationErrors)
                {
                    LogUtil(logger, current);
                }

                foreach (var current in value.ValidationErrors)
                {
                    LogUtil(logger, current);
                }
            }
        }


        // -- specific exceptions.


        private static void Log(ILog logger, DbEntityValidationException ex)
        {
            if (ex == null) { return; }

            foreach (var current in ex.EntityValidationErrors)
            {
                LogUtil(logger, current);
            }

            LogAny(logger, ex);
        }

        #endregion


        /// <summary>
        /// Log the specified SmtpException.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="ex"></param>
        private static void Log(ILog logger, SmtpException ex)
        {
            if (ex == null) { return; }

            logger.WarnFormat("StatusCode: {0}", ex.StatusCode);

            LogAny(logger, ex);
        }


        /// <summary>
        /// Log the specified SoapException.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="ex"></param>
        private static void Log(ILog logger, SoapException ex)
        {
            if (ex == null) { return; }

            logger.WarnFormat("{0}Code: {1}{0}Actor {2}{0}Detail: {3}{0}Lang: {4}{0}Node: {5}{0}Role: {6}{0}SubCode: {7}{0}Message{8}{0}"
                , Environment.NewLine
                , ex.Code
                , ex.Actor
                , ex.Detail.OuterXml
                , ex.Lang
                , ex.Node
                , ex.Role
                , ex.SubCode
                , ex.Message
                );

            LogAny(logger, ex);
        }


        private static void Log(ILog logger, SqlError sqlError)
        {
            logger.WarnFormat("SqlError: {0}", sqlError);
            logger.WarnFormat("SqlError: {0}, {1}, {2}, {3}, {4}, {5}, {6} ", sqlError.LineNumber, sqlError.Message, sqlError.Number, sqlError.Procedure, sqlError.Server, sqlError.Source, sqlError.State);
        }


        // Optionally handle SqlTypeException

        /// <summary>
        /// Log the specified SqlException.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="ex"></param>
        private static void Log(ILog logger, SqlException ex)
        {
            if (ex == null) { return; }

            var errors = new StringBuilder();

            foreach (SqlError error in ex.Errors)
            {
                Log(logger, error); // recursive!
            }

            try
            {
                logger.ErrorFormat(
                    "SqlException- {0}Class: {1}{0}Errors: {2}{0}LindeNo: {3}{0}Number: {4}{0}Procedure: {5}{0}Server: {6}{0}Source: {7}{0}State {8}{0} "
                    , Environment.NewLine + "    "
                    , ex.Class
                    , errors
                    , ex.LineNumber
                    , ex.Number
                    , ex.Procedure
                    , ex.Server
                    , ex.Source
                    , ex.State
                    );
            }
            catch (Exception exx)
            {
                logger.Error(exx.Message, exx);
            }

            LogAny(logger, ex);
        }


        private static void Log(ILog logger, WebException ex)
        {
            if (ex == null) { return; }

            if (logger != null)
            {
                var t2 = ex.Response as FtpWebResponse;

                logger.ErrorFormat(
                    "WebException-  {3}, Status: '{2}', Response ContentType: '{0}',  - ResponseUri: {1}, Ftp: {4}, {5}, {6}, {7},"
                    //, ex.Response.ContentType
                    , t2 == null ? ex.Response.ContentType : ""
                    , ex.Response.ResponseUri
                   , ex.Status
                    , ex.Message
                    , t2 != null ? t2.BannerMessage : ""
                    , t2 != null ? t2.ExitMessage : ""
                    , t2 != null ? t2.StatusDescription : ""
                    , t2 != null ? t2.WelcomeMessage : ""
                );
            }

            LogAny(logger, ex);
        }


        private static void Log(ILog logger, DbUpdateException ex)
        {
            if (ex == null) { return; }

            if (logger != null)
            {
                logger.ErrorFormat("DbUpdateException-    DbEntityEntry: {0}", ex);
                foreach (DbEntityEntry ee in ex.Entries)
                {
                    logger.ErrorFormat("DbUpdateException-        Entry: {0}", ee);
                }
            }
        }


        private static void Log(ILog logger, UpdateException ex)
        {
            if (ex == null) { return; }

            if (logger != null)
            {
                logger.ErrorFormat("UpdateException-    DbEntityEntry: {0}", ex);
                foreach (ObjectStateEntry ee in ex.StateEntries)
                {
                    logger.ErrorFormat("UpdateException-        Entry: {0}", ee);
                }
            }
        }


        // -- Dispatcher

        /// <summary>
        /// Dispatch to more specific logger.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="ex"></param>
        private static void LogDispatch(ILog logger, Exception ex)
        {
            // Only add handlers for Exceptions having specific properties.

            var t1 = ex as DbEntityValidationException;
            var t2 = ex as SmtpException;
            var t3 = ex as SoapException;
            var t4 = ex as SqlException;
            var t5 = ex as WebException;
            var t6 = ex as DbUpdateException;
            var t7 = ex as UpdateException;


            if (t1 != null)
            {
                Log(logger, t1);
            }
            else if (t2 != null)
            {
                Log(logger, t2);
            }
            else if (t3 != null)
            {
                Log(logger, t3);
            }
            else if (t4 != null)
            {
                Log(logger, t4);
            }
            else if (t5 != null)
            {
                Log(logger, t5);
            }
            else if (t6 != null)
            {
                Log(logger, t6);
            }
            else if (t7 != null)
            {
                Log(logger, t7);
            }
            else
            {
                LogAny(logger, ex);
            }
        }


        // -- Public 

        /// <summary>
        /// Log the specified Exception and inner exceptions.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="ex"></param>
        public static void LogException(ILog logger, Exception ex)
        {
            if (logger == null) { return; }

            try
            {
                var ix = ex;
                var maxLoops = 10;
                while (true)
                {
                    if (maxLoops-- <= 0) { break; } // prevent unlikely inifinite loop

                    if (ix != null) // stops iteration after last inner exception.
                    {
                        LogDispatch(logger, ix);
                        ix = ix.InnerException;
                        continue;
                    }
                    break;
                }
            }
            catch (Exception exx)
            {
                try
                {
                    logger.Error("Error in logging", exx);
                }
                catch
                {
                    // no action
                }
            }
        }


        /// <summary>
        /// Extension variant!
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="logger"></param>
        public static void LogException(this Exception ex, ILog logger)
        {
            LogException(logger, ex);
        }


    }
}