function slider(flag, num) {
  var current = $(".item.current"),
      next,
      index;
  if (!flag) {
    next = current.is(":last-child") ? $(".item").first() : current.next();
    index = next.index();
  } else if (flag === 'dot') {
    next = $(".item").eq(num);
    index = num;
  } else {
    next = current.is(":first-child") ? $(".item").last() : current.prev();
    index = next.index();
  }
  next.addClass("current");
  current.removeClass("current");
  $(".dot").eq(index).addClass("current").siblings().removeClass("current");
}
var setSlider = setInterval(slider, 4000);

$(".button").on("click", function() {
  clearInterval(setSlider);
  var flag = $(this).is(".prev") ? true : false;
  slider(flag);
  setSlider = setInterval(slider, 4000);
});

$(".dot").on("click", function() {
  if ($(this).is(".current")) return;
  clearInterval(setSlider);
  var num = $(this).index();
  slider('dot', num);
  setSlider = setInterval(slider, 4000);
});