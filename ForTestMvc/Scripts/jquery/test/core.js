/**
* test for $('div').find('');
*
**/

// Global undefined variable
window.undefined = window.undefined;

// note: 1. jQuery容器定义及单例模式逻辑。如在 document 域中创建一个 jQuery 实例对象
function jQuery(a, c) {

    // Shortcut for document ready (because $(document).each() is silly)
    // note: usage eg.. $(function(){}).ready();
    if (a && a.constructor == Function && jQuery.fn.ready)
        return jQuery(document).ready(a);

    // Make sure that a selection was provided
    a = a || jQuery.context || document;

    // Watch for when a jQuery object is passed as the selector
    if (a.jquery)
        return $(jQuery.merge(a, []));

    // Watch for when a jQuery object is passed at the contextl
    if (c && c.jquery)
        return $(c).find(a);

    // If the context is global, return a new object
    if (window == this)
        return new jQuery(a, c);

    // Handle HTML strings
    var m = /^[^<]*(<.+>)[^>]*$/.exec(a);
    if (m) a = jQuery.clean([m[1]]);

    // Watch for when an array is passed in
    this.get(a.constructor == Array || a.length && !a.nodeType && a[0] != undefined && a[0].nodeType ?
		// Assume that it is an array of DOM Elements
		jQuery.merge(a, []) :

		// Find the matching elements and save them for later
		jQuery.find(a, c));

    // See if an extra function was provided
    var fn = arguments[arguments.length - 1];

    // If so, execute it in context
    if (fn && fn.constructor == Function)
        this.each(fn);
}

// Map over the $ in case of overwrite
if ($)
    jQuery._$ = $;

// Map the jQuery namespace to the '$' one
var $ = jQuery;


// note: 2. 原型容器初始方法集。非静态方法，某些实现需要依赖拓展的静态方法
jQuery.fn = jQuery.prototype = {
    jquery: "$Rev: 509 $",
    
	// note: num 是个元素
    get: function (num) {
        // Watch for when an array (of elements) is passed in
        if (num && num.constructor == Array) {

            // Use a tricky hack to make the jQuery object
            // look and feel like an array
            this.length = 0;
            [].push.apply(this, num);

            return this;
        } else
            return num == undefined ?

				// Return a 'clean' array
				jQuery.map(this, function (a) { return a }) :

				// Return just the object
				this[num];
    },
    
	text: function (e) {
        e = e || this;
        var t = "";
        for (var j = 0; j < e.length; j++) {
            var r = e[j].childNodes;
            for (var i = 0; i < r.length; i++)
                t += r[i].nodeType != 1 ?
					r[i].nodeValue : jQuery.fn.text([r[i]]);
        }
        return t;
    },
	
	each: function (fn, args) {
        return jQuery.each(this, fn, args);
    },

	find: function (t) {
        return this.pushStack(jQuery.map(this, function (a) {
            return jQuery.find(t, a);
        }), arguments);
    },
    
	pushStack: function (a, args) {
        var fn = args && args[args.length - 1];

        if (!fn || fn.constructor != Function) {
            if (!this.stack) this.stack = [];
            this.stack.push(this.get());
            this.get(a);
        } else {
            var old = this.get();
            this.get(a);
            if (fn.constructor == Function)
                return this.each(fn);
            this.get(old);
        }

        return this;
    }

};


// note: 3. 继承：容器拓展的实现机制
jQuery.extend = jQuery.fn.extend = function (obj, prop) {
    if (!prop) { prop = obj; obj = this; }
    for (var i in prop) obj[i] = prop[i];
    return obj;
};


// note: 4. 静态容器拓展方法集，作为基本工具集 utils（选择、过滤、事件等）
jQuery.extend({
	
    each: function (obj, fn, args) {
        if (obj.length == undefined)
            for (var i in obj)
                fn.apply(obj[i], args || [i, obj[i]]);
        else
            for (var i = 0; i < obj.length; i++)
                fn.apply(obj[i], args || [i, obj[i]]);
        return obj;
    },

    className: {
        add: function (o, c) {
            if (jQuery.className.has(o, c)) return;
            o.className += (o.className ? " " : "") + c;
        },
        remove: function (o, c) {
            o.className = !c ? "" :
				o.className.replace(
					new RegExp("(^|\\s*\\b[^-])" + c + "($|\\b(?=[^-]))", "g"), "");
        },
        has: function (e, a) {
            if (e.className != undefined)
                e = e.className;
            return new RegExp("(^|\\s)" + a + "(\\s|$)").test(e);
        }
    },

    expr: {
        "": "m[2]== '*'||a.nodeName.toUpperCase()==m[2].toUpperCase()",
        "#": "a.getAttribute('id')&&a.getAttribute('id')==m[2]",
        ":": {
            // Position Checks
            lt: "i<m[3]-0",
            gt: "i>m[3]-0",
            nth: "m[3]-0==i",
            eq: "m[3]-0==i",
            first: "i==0",
            last: "i==r.length-1",
            even: "i%2==0",
            odd: "i%2",

            // Child Checks
            "first-child": "jQuery.sibling(a,0).cur",
            "last-child": "jQuery.sibling(a,0).last",
            "only-child": "jQuery.sibling(a).length==1",

            // Parent Checks
            parent: "a.childNodes.length",
            empty: "!a.childNodes.length",

            // Text Check
            contains: "(a.innerText||a.innerHTML).indexOf(m[3])>=0",

            // Visibility
            visible: "a.type!='hidden'&&jQuery.css(a,'display')!='none'&&jQuery.css(a,'visibility')!='hidden'",
            hidden: "a.type=='hidden'||jQuery.css(a,'display')=='none'||jQuery.css(a,'visibility')=='hidden'",

            // Form elements
            enabled: "!a.disabled",
            disabled: "a.disabled",
            checked: "a.checked",
            selected: "a.selected"
        },
        ".": "jQuery.className.has(a,m[2])",
        "@": {
            "=": "z==m[4]",
            "!=": "z!=m[4]",
            "^=": "!z.indexOf(m[4])",
            "$=": "z.substr(z.length - m[4].length,m[4].length)==m[4]",
            "*=": "z.indexOf(m[4])>=0",
            "": "z"
        },
        "[": "jQuery.find(m[2],a).length"
    },
    
    // The regular expressions that power the parsing engine
    parse: [
		// Match: [@value='test'], [@foo]
		["\\[ *(@)S *([!*$^=]*) *Q\\]", 1],

		// Match: [div], [div p]
		["(\\[)Q\\]", 0],

		// Match: :contains('foo')
		["(:)S\\(Q\\)", 0],

		// Match: :even, :last-chlid
		["([:.#]*)S", 0]
    ],
	
	token: [
		"\\.\\.|/\\.\\.", "a.parentNode",
		">|/", "jQuery.sibling(a.firstChild)",
		"\\+", "jQuery.sibling(a).next",
		"~", function (a) {
		    var r = [];
		    var s = jQuery.sibling(a);
		    if (s.n > 0)
		        for (var i = s.n; i < s.length; i++)
		            r.push(s[i]);
		    return r;
		}
    ],
	
    getAll: function (o, r) {
        r = r || [];
        var s = o.childNodes;
        for (var i = 0; i < s.length; i++)
            if (s[i].nodeType == 1) {
                r.push(s[i]);
                jQuery.getAll(s[i], r);
            }
        return r;
    },

    filter: function (t, r, not) {
        // Figure out if we're doing regular, or inverse, filtering
        var g = not !== false ? jQuery.grep :
			function (a, f) { return jQuery.grep(a, f, true); };

        while (t && /^[a-z[({<*:.#]/i.test(t)) {

            var p = jQuery.parse;

            for (var i = 0; i < p.length; i++) {
                var re = new RegExp("^" + p[i][0]

					// Look for a string-like sequence
					.replace('S', "([a-z*_-][a-z0-9_-]*)")

					// Look for something (optionally) enclosed with quotes
					.replace('Q', " *'?\"?([^'\"]*?)'?\"? *"), "i");

                var m = re.exec(t);

                if (m) {
                    // Re-organize the match
                    if (p[i][1])
                        m = ["", m[1], m[3], m[2], m[4]];

                    // Remove what we just matched
                    t = t.replace(re, "");

                    break;
                }
            }

            // :not() is a special case that can be optomized by
            // keeping it out of the expression list
            if (m[1] == ":" && m[2] == "not")
                r = jQuery.filter(m[3], r, false).r;

                // Otherwise, find the expression to execute
            else {
                var f = jQuery.expr[m[1]];
                if (f.constructor != String)
                    f = jQuery.expr[m[1]][m[2]];

                // Build a custom macro to enclose it
                eval("f = function(a,i){" +
					(m[1] == "@" ? "z=jQuery.attr(a,m[3]);" : "") +
					"return " + f + "}");

                // Execute it against the current filter
                r = g(r, f);
            }
        }

        // Return an array of filtered elements (r)
        // and the modified expression string (t)
        return { r: r, t: t };
    },

    grep: function (elems, fn, inv) {
        // If a string is passed in for the function, make a function
        // for it (a handy shortcut)
        if (fn.constructor == String)
            fn = new Function("a", "i", "return " + fn);

        var result = [];

        // Go through the array, only saving the items
        // that pass the validator function
        for (var i = 0; i < elems.length; i++)
            if (!inv && fn(elems[i], i) || inv && !fn(elems[i], i))
                result.push(elems[i]);

        return result;
    },
	
    find: function (t, context) {
        // Make sure that the context is a DOM Element
        if (context && context.nodeType == undefined)
            context = null;

        // Set the correct context (if none is provided)
        context = context || jQuery.context || document;

        if (t.constructor != String) return [t];

        if (!t.indexOf("//")) {
            context = context.documentElement;
            t = t.substr(2, t.length);
        } else if (!t.indexOf("/")) {
            context = context.documentElement;
            t = t.substr(1, t.length);
            // FIX Assume the root element is right :(
            if (t.indexOf("/") >= 1)
                t = t.substr(t.indexOf("/"), t.length);
        }

        var ret = [context];
        var done = [];
        var last = null;

        while (t.length > 0 && last != t) {
            var r = [];
            last = t;

            t = jQuery.trim(t).replace(/^\/\//i, "");

            var foundToken = false;

            for (var i = 0; i < jQuery.token.length; i += 2) {
                var re = new RegExp("^(" + jQuery.token[i] + ")");
                var m = re.exec(t);

                if (m) {
                    r = ret = jQuery.map(ret, jQuery.token[i + 1]);
                    t = jQuery.trim(t.replace(re, ""));
                    foundToken = true;
                }
            }

            if (!foundToken) {
                if (!t.indexOf(",") || !t.indexOf("|")) {
                    if (ret[0] == context) ret.shift();
                    done = jQuery.merge(done, ret);
                    r = ret = [context];
                    t = " " + t.substr(1, t.length);
                } else {
                    var re2 = /^([#.]?)([a-z0-9\\*_-]*)/i;
                    var m = re2.exec(t);

                    if (m[1] == "#") {
                        // Ummm, should make this work in all XML docs
                        var oid = document.getElementById(m[2]);
                        r = ret = oid ? [oid] : [];
                        t = t.replace(re2, "");
                    } else {
                        if (!m[2] || m[1] == ".") m[2] = "*";

                        for (var i = 0; i < ret.length; i++)
                            r = jQuery.merge(r,
								m[2] == "*" ?
									jQuery.getAll(ret[i]) :
									ret[i].getElementsByTagName(m[2])
							);
                    }
                }
            }

            if (t) {
                var val = jQuery.filter(t, r);
                ret = r = val.r;
                t = jQuery.trim(val.t);
            }
        }

        if (ret && ret[0] == context) ret.shift();
        done = jQuery.merge(done, ret);

        return done;
    },

    merge: function (first, second) {
        var result = [];

        // Move b over to the new array (this helps to avoid
        // StaticNodeList instances)
        for (var k = 0; k < first.length; k++)
            result[k] = first[k];

        // Now check for duplicates between a and b and only
        // add the unique items
        for (var i = 0; i < second.length; i++) {
            var noCollision = true;

            // The collision-checking process
            for (var j = 0; j < first.length; j++)
                if (second[i] == first[j])
                    noCollision = false;

            // If the item is unique, add it
            if (noCollision)
                result.push(second[i]);
        }

        return result;
    },

    trim: function (t) {
        return t.replace(/^\s+|\s+$/g, "");
    },

    map: function (elems, fn) {
        // If a string is passed in for the function, make a function
        // for it (a handy shortcut)
        if (fn.constructor == String)
            fn = new Function("a", "return " + fn);

        var result = [];

        // Go through the array, translating each of the items to their
        // new value (or values).
        for (var i = 0; i < elems.length; i++) {
            var val = fn(elems[i], i);

            if (val !== null && val != undefined) {
                if (val.constructor != Array) val = [val];
                result = jQuery.merge(result, val);
            }
        }

        return result;
    },


});





