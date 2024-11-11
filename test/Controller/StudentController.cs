using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test.Controller
{
    // Controllers/StudentController.cs
    public class StudentController
    {
        public string GetStudentInfo()
        {
            return @"
HTTP/1.1 200 OK
Content-Type: text/html

<!DOCTYPE html>
<html lang='en'>
<head>
    <meta charset='UTF-8'>
    <title>Student Info</title>
</head>
<body>
    <h1>Student Information</h1>
    <p>Name: Pham Thanh Truc</p>
    <p>ID: 22123164</p>
    <p>PC: PC01</p>
</body>
<a href='/login'>Login Here</a>
</html>";
        }
    }
}
