/*
* Create a simple select component.
* Auth:
*       By Catom.
* Usage ：
*       1. load jquery first;
*/


// 组件对象，将所有调用的组件类的对象封装于此对象中。
window.skyComponent = window.skyComponent || {};

// js 类（方法体）继承的方法
skyComponent.extendClass = function (baseClass, funcClasses) {
    for (var i = 0; i < funcClasses.length; i++) {
        baseClass.prototype[i] = funcClasses[i];
    }
    baseClass.thisClass = baseClass.prototype;
}












