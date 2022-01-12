﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentBook.Domain.ApiModel.ResponseApiModels
{
    public class ResponseApiModel<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public ResponseApiModel(bool isSuccess = false, string message = null)
        {
            Success = isSuccess;
            Message = message;
        }

        public ResponseApiModel(T data, bool isSuccess, string message = null)
        {
            Success = isSuccess;
            Message = message;
            Data = data;
        }
    }
}
