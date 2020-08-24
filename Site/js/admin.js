
function AddValue(from, to)
{
    var f = document.getElementById(from);
    var t = document.getElementById(to);
    if (!f || !t)
        return;
    for (var i = 0; i < f.options.length; i++)
    {
        var opt = f.options[i];
        if (opt.selected && opt.style.display != 'none')
        {
            if (!IsPresent(t, opt.value))
            {
                var oOption = document.createElement("OPTION");
                t.options.add(oOption);
                oOption.innerHTML = opt.innerHTML;
                oOption.value = opt.value;
            }
        }
    }
    
    SetChange();
}

function collectData(obj1, obj2, obj3, obj4, out)
{

    var str = getMealInfo(obj1);
    str += getMealInfo(obj2);
    str += getMealInfo(obj3);
    str += getMealInfo(obj4);

    str = str.substring(0, str.length - 1);

    var o = document.getElementById(out);
    if (!o)
    {
        alert(out);
        return;
    }

    o.value = str;
}

function getMealInfo(obj)
{
    var res = "";
    var o = document.getElementById(obj);
    if (!o)
        return "";
    for (var i = 0; i < o.options.length; i++)
    {
        res += o.options[i].value + ",";
    }
    return res;
}

function IsPresent(obj, val)
{
    for (var i = 0; i < obj.options.length; i++)
    {
        var opt = obj.options[i];
        if (opt.value == val)
        {
            return true;
        }
    }
    return false;
}

function DelValue(elem)
{
    var f = document.getElementById(elem);
    if (!f)
        return;
    for (var i = f.options.length - 1; i >= 0; i--)
    {
        var opt = f.options[i];
        if (opt.selected)
        {
            f.options[i] = null;
        }
    }
    
    SetChange();
}

function FilterList(obj, filter)
{    
    var txt = obj.value;
    var o = document.getElementById(filter);
    if (!o)
        return;
    for (var i = o.options.length - 1; i >= 0; i--)
    {
        var opt = o.options[i];
        if  (txt.length == "")
        {
            opt.style.display = '';
        }
        else if (!Find(opt.innerHTML, txt))
        {
            opt.selected = false;
            opt.style.display = 'none';
        }
        else
        {
            opt.style.display = '';
        }
    }
}

function Find (txt, search)
{
    var re = new RegExp(search, "i");
    var res = true;
    if (txt.search(re) == -1)
    {
        res = false;
    }
    return res;
}

function Confirm()
{
    return window.confirm("Delete ?");
}

function SetChange()
{
	var changeField = document.getElementById("hdnChange");
	if (changeField) 
	{
	    changeField.value = "true";
	}
}