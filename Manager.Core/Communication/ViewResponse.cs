using System;
using System.Collections.Generic;
using Manager.Core.Extensions;

namespace Manager.Core.Communication
{
    public class ViewResponse
    {
        public static object Error(object data, string message = null)
        {
            return Response(data, false, message ?? "Erro ao tentar realizar operação");
        }

        public static object AuthorizationError(string message = null)
        {
            return Response(null, false, message ?? "Não Autorizado");
        }

        public static object Success(object data, string message = null)
        {
            return Response(data, true, message ?? "Operação realizada com sucesso");
        }

        private static object Response(object data, bool success, string message)
        {
            return new
            {
                Message = message,
                Success = success,
                Data = data
            };
        }
    }
}