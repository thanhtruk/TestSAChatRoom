using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test.Utility
{
    // Utilities/RequestHelper.cs
    public static class RequestHelper
    {
        // Extracts form data based on a specified key
        public static string ExtractFormData(string request, string key)
        {
            // Parse form data from POST request
            // For simplicity, parse manually here
            string[] split = request.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.None);
            if (split.Length > 1)
            {
                string[] formData = split[1].Split('&');
                foreach (var data in formData)
                {
                    var keyValue = data.Split('=');
                    if (keyValue.Length == 2 && keyValue[0] == key)
                    {
                        return keyValue[1];
                    }
                }
            }
            return null;
        }
        
        public static string ExtractCookie(string request)
        {
            const string sessionTokenKey = "sessionToken=";
            string[] headers = request.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var header in headers)
            {
                if (header.StartsWith("Cookie:"))
                {
                    // Tìm vị trí bắt đầu của sessionToken trong cookie header
                    int tokenStartIndex = header.IndexOf(sessionTokenKey);
                    if (tokenStartIndex != -1)
                    {
                        tokenStartIndex += sessionTokenKey.Length;

                        // Tìm vị trí kết thúc của sessionToken (trước dấu chấm phẩy hoặc hết chuỗi)
                        int tokenEndIndex = header.IndexOf(';', tokenStartIndex);
                        if (tokenEndIndex == -1)
                        {
                            tokenEndIndex = header.Length;
                        }

                        // Trả về sessionToken đã được trích xuất
                        return header.Substring(tokenStartIndex, tokenEndIndex - tokenStartIndex);
                    }
                }
            }

            // Trả về null nếu không tìm thấy sessionToken
            return null;
        }

    }

}
