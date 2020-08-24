function emailCheck (emailStr)
{
    var emailPat=/^(.+)@(.+)$/;
    var specialChars="\\(\\)<>@,;:\\\\\\\"\\.\\[\\]";
    var validChars="\[^\\s" + specialChars + "\]";
    var quotedUser="(\"[^\"]*\")";
    var ipDomainPat=/^\[(\d{1,3})\.(\d{1,3})\.(\d{1,3})\.(\d{1,3})\]$/;
    var atom=validChars + '+';
    var word="(" + atom + "|" + quotedUser + ")";
    // The following pattern describes the structure of the user
    var userPat=new RegExp("^" + word + "(\\." + word + ")*$");
    var domainPat=new RegExp("^" + atom + "(\\." + atom +")*$");
    var matchArray=emailStr.match(emailPat);
    if (matchArray == null)
    {
        message("Email address seems incorrect (check @ and .'s)");
        return false;
    }
    var user=matchArray[1];
    var domain=matchArray[2];
    if (user.match(userPat)==null)
    {
    // user is not valid
        message("The username doesn't seem to be valid.");
        return false;
    }
    var IPArray=domain.match(ipDomainPat)
    if (IPArray!=null)
    {
    // this is an IP address
        for (var i=1;i<=4;i++)
        {
            if (IPArray[i]>255)
            {
                message("Destination IP address is invalid!");
                return false;
            }
        }
        return true;
    }
    var domainArray=domain.match(domainPat);
    if (domainArray==null)
    {
        message("The domain name doesn't seem to be valid.");
        return false;
    }
    var atomPat=new RegExp(atom,"g");
    var domArr=domain.match(atomPat);
    var len=domArr.length;
    if (domArr[domArr.length-1].length<2 || domArr[domArr.length-1].length>3)
    {
        message("The address must end in a three-letter domain, or two letter country.");
        return false;
    }
    if (len<2)
    {
        var errStr="This address is missing a hostname!";
        message(errStr);
        return false;
    }
    return true;
}

function message(message)
{
    alert(message);
}

function emailCheckTF (emailStr1, emailStr2)
{
    var email1 = emailCheck(emailStr1);
    if (!email1) return false;
    var email2 = emailCheck (emailStr2);
    if (!email2) return false;

    return true;
}
