<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tester.aspx.cs" Inherits="RestCake.AddressBook.Services.tester" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" action="contacts/book" method="post">
        <input type="hidden" name="_method" value="delete" />
        <input name="book_name" />
        <input type="submit" />
    </form>
</body>
</html>
