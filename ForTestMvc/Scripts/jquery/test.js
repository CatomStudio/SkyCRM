
var obj = function () { return { name: 'catom' } };

// based on jquery.
function func(a, b, c) {
    console.log(this);
    console.log(arguments[0])
    console.log( a, b, c);
}

// test bind
var inst = func.bind(null, obj);
inst('argA');
// ===> 等同于
func.call(null, obj, 'argA');


func.__proto__ = function () {
    console.log('__proto__方法');
}

func.fn = func.prototype = function () {
    console('prototype');
}








