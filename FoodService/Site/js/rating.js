function loadRatings() {
    $('.rating .likes').click(function (event) {
        event.stopPropagation();

        var dishId = $(this).closest('.dish-element').attr('data-id');
        setRating( dishId, true );
    });

    $('.rating .dislikes').click(function (event) {
        var dishId = $(this).closest('.dish-element').attr('data-id');
        setRating(dishId, false);
    });

    updateRatings();
}

function setRating(dishId, isLike) {
    $.post('AjaxHandler.ashx?action=rating', { userId: currentUser, dishId: dishId, isLike: isLike }, function (data) {
        var dish = $('.dish-element[data-id=' + dishId + ']');
        
        var rating = data.split(',');

        dish.find('.rating .likes i').html(rating[0]);
        dish.find('.rating .dislikes i').html(rating[1]);
        dish.find('.rating').attr('data-user', rating[2]);

        updateRatings();
    });

    event.stopPropagation();
}

function updateRatings() {
    $('.rating .likes').removeClass('selected');
    $('.rating .dislikes').removeClass('selected');

    $('.rating').each(function () {
        var isUser = $(this).attr('data-user');
        if (isUser == 'true') {
            $(this).children('.likes').addClass('selected');
        } else if (isUser == 'false') {
            $(this).children('.dislikes').addClass('selected');
        }
    });
}
