
function addRow(table, foodsArray)
{
    var tr = table.insertRow(-1);
    var rowcount = table.rows.length - 1;
    for(var i = 0; i < 4; i++)
    {
        var td = tr.insertCell(-1);
        var select = document.createElement("SELECT");
        select.id = "meal" + rowcount + i;
        if ((i == 2) || (i == 3))
        {
            if (i == 2)
            {
                fillSelect(select, 3);
            }
            else
            {
                fillSelect(select, 2);
            }
        }
        else
        {
            fillSelect(select, i);
        }
        if (foodsArray.length != 0)
        {
            changeIndex(select, foodsArray)
        }
        td.appendChild(select);
    }
}

function changeIndex(select, foodsArray)
{
    var options = select.options;
    for (var i = 1; i < options.length; i++)
    {
        for (var j = 0; j < foodsArray.length; j++)
        {
            if (options[i].value == foodsArray[j])
            {
                select.selectedIndex = i;
                return;
            }
        }
    }
}

function fillSelect(obj, pos)
{
    var oOption = document.createElement("OPTION");
    obj.options.add(oOption);
    oOption.value = 0;
    oOption.innerHTML = "";
    
    for (i in data[pos])
    {
        var oOption = document.createElement("OPTION");
        obj.options.add(oOption);
        oOption.value = data[pos][i][0];
        oOption.innerHTML = data[pos][i][1];
    }
}

function createOrderRow()
{
    var o = document.getElementById("testTable");
    
    if (!o)
    {
        return;
    }
    else
    {
        addRow(o, new Array());
    }
}

function getPreviousOrder(previousOrderElemId)
{
    var o = document.getElementById("testTable")
    if (!o)
    {
        return;
    }
    else
    {
        createPreviousOrder(o, previousOrderElemId);
    }
}

function createPreviousOrder(table, previousOrderElemId)
{
    var dataElem = document.getElementById(previousOrderElemId);
    var data = new String(dataElem.value);
    var dataArray = data.split(";");
    for (var i = 0; i < dataArray.length; i++)
    {
        addRow(table, dataArray[i].split(","));
    }
}


function getData(dataElem)
{
    var o = document.getElementById("testTable");
    
    if (!o)
    {
        return;
    }
    else
    {
        getString(o, dataElem);
    }
}

function getString(table, dataElem)
{
    var str = new String("");
    for (var i = 1; i < table.rows.length; i++)
    {
        var flag = false;
        var tempstr = new String("");
        for (var j = 0; j < 4; j++)
        {
            var elem = document.getElementById("meal" + i + "" + j)
            if (elem.value != 0)
            {
                flag = true;
            }
            tempstr += elem.value;
            tempstr += ",";
        }
        
        if (flag)
        {
            tempstr = tempstr.substring(0, tempstr.length - 1);
            str += tempstr + ";";
        }
    }
    str = str.substring(0, str.length - 1);
    var o = document.getElementById(dataElem);
    o.value = str;
}

