var dishes = new Array();

var order_salat = new Array();
var order_soup = new Array();
var order_garnish_sd = new Array();

var order_changed = false;

function AddDish(Id, Name, ImagePathBig, ImagePathSmall, Cost, Description, Single,  Type, Weight, SupplierId)
{
    var dish = new Array(Id, Name, ImagePathBig, Cost, Description, Single, Type, Weight, SupplierId);
    dishes.push(dish);
}

function GetDishById(id)
{
    for(var i = 0; i < dishes.length; i++)
    {
        if (id == dishes[i][0])
        {
            return dishes[i];
        }
    }
    return false;
}

function addDishToOrderLoad(id)
{
    var elem = document.getElementById("dish" + id);
    if (elem) {
        addDishToOrder(elem, id);
    }
    order_changed = false;
}

function addPopover(element)
{
    var dish = GetDishById(element.attr('data-id'));
    var content = '';
    if (dish[4] && dish[4] != '') { content += dish[4] + "<br/>" };
    if (dish[2]) { content += "<img style='width: 100%;' src='" + dish[2] + "'/><br/>" };
    if (dish[7]) { content += "Вес: " + dish[7] + "г" + "<br/>" };
    element.popover({ content: content, html: true, placement: 'auto' });
}

function addDishToOrder(elem, id)
{
    order_changed = true;
    var dish = GetDishById(id);
    if (dish != false)
    {
        if (dish[6] == 0)
        {
            var obj = getOrderObject(elem, id);
            CopyDish("salatOrder", obj);
            order_salat.push([dish, obj]);
        }
        else if (dish[6] == 1)
        {
            var obj = getOrderObject(elem, id);
            CopyDish("soupOrder", obj);
            order_soup.push([dish,obj]);
        }
        else if (dish[6] == 3)
        {
            addSecondDish(elem, id, dish);
        }
        else
        {
            addGarnish(elem, id, dish);
        }
    }
    updateCursor(elem,1);
    updateSumm();

    addPopover($('.table-dish #dish' + id));
}


function updateSumm() {
	var summ = 0;
	$( '.order-bg .dish-element' ).each(function( index ) {
		var id = $( this ).attr('id').replace("dish", "");
		var dish = GetDishById(id);
		if (dish) {
            summ += parseFloat(dish[3].replace(/[,]+/g, '.'));
		}
    });

    $("#orderSumm").text(summ.toFixed(2));

    if (summ > 300) {
        $("#orderSumm").css('color', '#ff0000');
    }
}
	
function getOrderObject(elem, id)
{
    var obj = elem.cloneNode(true);
    if (flag)
    {
        obj.onclick = function(){RemoveObj(obj, id)};
    }
    else
    {
        obj.onclick = function(){};
    }
    return obj;
}

function removeElem(aElem) {
    aElem.parentNode.removeChild(aElem);
}

function RemoveObj(elem, id)
{
    $(elem).popover('destroy');
    var dish = GetDishById(id);
    updateCursor(elem,1);
    if (dish != false)
    {
        if (dish[6] == 0)
        {
            RemoveFromArr(order_salat, elem);
            removeElem(elem);
        }
        else if (dish[6] == 1)
        {
            RemoveFromArr(order_soup, elem);
            removeElem(elem);
        }
        else if (dish[6] == 3)
        {
            RemoveSecondDish(dish, elem, id);
        }
        else
        {
            RemoveGarnish(dish, elem, id);
        }
    }
    updateSumm();
}

function RemoveFromArr(arr, elem)
{
    for (var i = 0; i < arr.length; i++)
    {
        if (arr[i][1] == elem)
        {
            arr.splice(i,1);
            break;
        }
    }
}

function FindDishInArray(arr, elem)
{
    for (var i = 0; i < arr.length; i++)
    {
        if (arr[i][1] == elem)
        {
            return arr[i];
        }
    }
    return -1;
}

function FindGarnishInArray(arr, elem)
{
    for (var i = 0; i < arr.length; i++)
    {
        if (arr[i][1][1] == elem)
        {
            return i;
        }
    }
    return -1;
}

function FindSDInArray(arr, elem)
{
    for (var i = 0; i < arr.length; i++)
    {
        if (arr[i][0][1] == elem)
        {
            return i;
        }
    }
    return -1;
}


function addSecondDish(elem, id, dish)
{
    var obj = getOrderObject(elem, id);
    if (dish[5] == "True")
    {
        CopyDish("secondDishOrder", obj);
        var NoGarnishBox = getNoGarnishBox();
        CopyDish("garnishOrder", NoGarnishBox);
        order_garnish_sd.push(new Array([dish, obj], [-1, NoGarnishBox]));
    }
    else
    {
        var pos = findFreeGarnishPos(dish);
        if (pos == -1)
        {
            CopyDish("secondDishOrder", obj);
            var emptyBox = getEmptyBox();
            CopyDish("garnishOrder", emptyBox);
            order_garnish_sd.push(new Array([dish, obj], [0, emptyBox]));
        }
        else
        {
            order_garnish_sd[pos][0][0] = dish;
            ReplaceDish("secondDishOrder", order_garnish_sd[pos][0][1], obj);
            order_garnish_sd[pos][0][1] = obj;
        }
    }
}

function ReplaceDish(where, from, to)
{
    obj = document.getElementById(where);
    if (obj)
    {
        res = obj.insertBefore(to, from);
        removeElem(from);
    }
}

function getNoGarnishBox()
{
    obj = document.getElementById("NoGarnish");
    if (obj)
    {
        obj = obj.cloneNode(true);
        obj.style.display = "block";
        obj.id = "";
    }
    return obj;
}

function getEmptyBox()
{
    obj = document.getElementById("EmptyBox");
    var newObj = null;
    if (obj)
    {
        newObj = obj.cloneNode(true);
        newObj.style.display = "block";
        newObj.id = "";
    }
    return newObj;
}

function findFreeGarnishPos(dish)
{
    for(var i = 0; i < order_garnish_sd.length; i++)
    {
        if (order_garnish_sd[i][0][0] == 0 && order_garnish_sd[i][1][0][8] == dish[8])
        {
            return i;
        }
    }
    return -1;
}

function findFreeSDPos(dish)
{
    for(var i = 0; i < order_garnish_sd.length; i++)
    {
        if (order_garnish_sd[i][1][0] == 0 && order_garnish_sd[i][0][0][8] == dish[8])
        {
            return i;
        }
    }
    return -1;
}

function addGarnish(elem, id, dish)
{
    var obj = getOrderObject(elem, id);
    var pos = findFreeSDPos(dish);
    if (pos == -1)
    {
        CopyDish("garnishOrder", obj);
        var emptyBox = getEmptyBox();
        CopyDish("secondDishOrder", emptyBox);
        order_garnish_sd.push(new Array([0, emptyBox], [dish, obj]));
    }
    else
    {
        order_garnish_sd[pos][1][0] = dish;
        ReplaceDish("garnishOrder", order_garnish_sd[pos][1][1], obj);
        order_garnish_sd[pos][1][1] = obj;
    }
}


function RemoveSecondDish(dish, elem, id)
{
    var pos = FindSDInArray(order_garnish_sd, elem);
    if (pos == -1)
        return;
    var arr = order_garnish_sd[pos];
    if (arr[1][0] == -1 || arr[1][0] == 0)
    {
        removeElem(arr[1][1]);
        removeElem(arr[0][1]);
        order_garnish_sd.splice(pos, 1);
    }
    else if (true)
    {
        var empty = getEmptyBox();
        ReplaceDish("secondDishOrder", elem, empty);
        arr[0][0] = 0;
        arr[0][1] = empty;
    }

}

function RemoveGarnish(dish, elem, id)
{
    var pos = FindGarnishInArray(order_garnish_sd, elem);
    if (pos == -1)
        return;
    var arr = order_garnish_sd[pos];
    if (arr[0][0] == 0)
    {
        removeElem(arr[1][1]);
        removeElem(arr[0][1]);
        order_garnish_sd.splice(pos, 1);
    }
    else if (true)
    {
        var empty = getEmptyBox();
        ReplaceDish("garnishOrder", elem, empty);
        arr[1][0] = 0;
        arr[1][1] = empty;
    }
}

function CopyDish(container, obj)
{
    var oContainer = document.getElementById(container);
    if (oContainer)
    {
        oContainer.appendChild(obj);
    }
    else
    {
        alert("Error");
    }

}

function Save(obj)
{
    var obj = document.getElementById(obj)
    if (obj)
    {
        var salat = getIdsList(order_salat);
        var soup = getIdsList(order_soup);
        var second = getSecondIdsLIst(order_garnish_sd);
        obj.value = salat + "|" + soup + "|" + second;
        return true;
    }
    return false;
}

function getSecondIdsLIst(arr)
{
    var res = "";
    for(var i = 0; i < arr.length; i++)
    {
        var id1 = (arr[i][0][0][0])? arr[i][0][0][0]:0;
        var id2 = (arr[i][1][0][0])? arr[i][1][0][0]:0;
        res += id1 + "." + id2 + ",";
    }

    if (res.length > 0)
    {
        res = res.substring(0, res.length-1);
    }

    return res;
}


function getIdsList(arr)
{
    var res = "";
    for(var i = 0; i < arr.length; i++)
    {
        res += arr[i][0][0] + ",";
    }
    if (res.length > 0)
    {
        res = res.substring(0, res.length-1);
    }
    return res;
}

function updateCursor(obj, evnt, id)
{
    obj.style.cursor = (evnt)? "": "pointer";
    if (evnt)
    {
        var elem1 = document.getElementById("DescriptionBlock");
        if (elem1)
            elem1.style.display = "none";
    }
    else
    {
        var img = document.getElementById("DescriptionBlockImg");
        var descr = document.getElementById("DescriptionBlockTxt");
        var elem1 = document.getElementById("DescriptionBlock");
        var dish = GetDishById(id);
        if (elem1 && descr && img && (dish[2] > "" || dish[4] > "" || dish[7] > ""))
        {
            if (dish[2] > "")
            {
                img.src = dish[2];
                img.style.display = "";
            }
            else
            {
                img.style.display = "none";
            }
            descr.innerHTML = dish[4] + "<br/> Вес: " + dish[7] + "г<br /> Стоимость: " + dish[3] + "р";

            var pos = getElementPosition(obj);
            elem1.style.top = pos[1] + "px";
            elem1.style.left = (pos[0] + 210) + "px";
            elem1.style.display = "";
        }
    }
}

function getElementPosition(elem)
{
   if (!elem)
    return;
   var w = elem.offsetWidth;
   var h = elem.offsetHeight;
   var l = 0;
   var t = 0;
   while (elem)
   {
       l += elem.offsetLeft;
       t += elem.offsetTop;
       elem = elem.offsetParent;
   }
   return new Array(l,t);
}


function checkOrder()
{
    if (order_changed)
    {
        if (window.confirm("Заказ не сохранён! \r\n OK - нет не переходи, я хочу заказать именно это \r\n Cancel - нет я это есть не буду."))
        {
            if (window.event)
            {
                window.event.returnValue = false;
            }
            return false;
        }
    }
    return true;
}

function SelectRandomOrder() {
	var maxAmount = 255;
	var currentAmount = 0;

	var salads = jQuery.grep(dishes, function( n, i ) {
	  return n[6] == 0;
	});

	var soups = jQuery.grep(dishes, function( n, i ) {
	  return n[6] == 1;
	});

	var seconds = jQuery.grep(dishes, function( n, i ) {
	  return n[6] == 3;
	});

	var garnishs = jQuery.grep(dishes, function( n, i ) {
	  return n[6] == 2;
	});

	var availabelOrders = [];
	salads.forEach(function(salad) {
		saladAmount = parseInt(salad[3]);

		if (saladAmount > maxAmount) {
			return;
		}
		soups.forEach(function(soup) {
			soupAmount = parseInt(soup[3]);
			if (saladAmount + soupAmount > maxAmount) {
				return;
			}
			seconds.forEach(function(second) {
				secondAmount = parseInt(second[3]);
				if (saladAmount + soupAmount + secondAmount > maxAmount) {
					return;
				}
				if (second[5] == "1") {
					var availabelOrder = {
						saladId: salad[0],
						soupId: soup[0],
						secondId: second[0],
						garnishId: 0,
					}
					availabelOrders.push(availabelOrder);
					return;
				}
				garnishs.forEach(function(garnish) {
					garnishAmount = parseInt(garnish[3]);
					if (saladAmount + soupAmount + secondAmount + garnishAmount > maxAmount) {
						return;
					}
					var availabelOrder = {
						saladId: salad[0],
						soupId: soup[0],
						secondId: second[0],
						garnishId: garnish[0],
					}
					availabelOrders.push(availabelOrder);
				});
			});
		});

		seconds.forEach(function (second) {
		    secondAmount = parseInt(second[3]);
		    if (saladAmount + secondAmount > maxAmount) {
		        return;
		    }
		    if (second[5] == "1") {
		        var availabelOrder = {
		            saladId: salad[0],
		            soupId: 0,
		            secondId: second[0],
		            garnishId: 0,
		        }
		        availabelOrders.push(availabelOrder);
		        return;
		    }
		    garnishs.forEach(function (garnish) {
		        garnishAmount = parseInt(garnish[3]);
		        if (saladAmount + secondAmount + garnishAmount > maxAmount) {
		            return;
		        }
		        var availabelOrder = {
		            saladId: salad[0],
		            soupId: 0,
		            secondId: second[0],
		            garnishId: garnish[0],
		        }
		        availabelOrders.push(availabelOrder);
		    });
		});
	});

	var randomOrder = GetRandomOrder(availabelOrders);
	$('.order-bg .dish-element').click();

	if (typeof randomOrder == "undefined") {
	    return;
	}

	addDishToOrderLoad(randomOrder.saladId);
    if (randomOrder.soupId > 0) {
        addDishToOrderLoad(randomOrder.soupId);
    }
    addDishToOrderLoad(randomOrder.secondId);
	if (parseInt(randomOrder.garnishId) > 0) {
		addDishToOrderLoad(randomOrder.garnishId);
	}

	function GetRandomOrder(availabelOrders) {
		var index = Math.floor(Math.random() * availabelOrders.length);
		return availabelOrders[index];
	}

}
