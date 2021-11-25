!function(e,t){"object"==typeof exports&&"undefined"!=typeof module?module.exports=t():"function"==typeof define&&define.amd?define(t):e.Popper=t()}(this,function(){"use strict";var n="undefined"!=typeof window&&"undefined"!=typeof document&&"undefined"!=typeof navigator,o=function(){for(var e=["Edge","Trident","Firefox"],t=0;t<e.length;t+=1)if(n&&0<=navigator.userAgent.indexOf(e[t]))return 1;return 0}();var i=n&&window.Promise?function(e){var t=!1;return function(){t||(t=!0,window.Promise.resolve().then(function(){t=!1,e()}))}}:function(e){var t=!1;return function(){t||(t=!0,setTimeout(function(){t=!1,e()},o))}};function a(e){return e&&"[object Function]"==={}.toString.call(e)}function y(e,t){if(1!==e.nodeType)return[];var n=e.ownerDocument.defaultView.getComputedStyle(e,null);return t?n[t]:n}function h(e){return"HTML"===e.nodeName?e:e.parentNode||e.host}function m(e){if(!e)return document.body;switch(e.nodeName){case"HTML":case"BODY":return e.ownerDocument.body;case"#document":return e.body}var t=y(e),n=t.overflow,o=t.overflowX,r=t.overflowY;return/(auto|scroll|overlay)/.test(n+r+o)?e:m(h(e))}function v(e){return e&&e.referenceNode?e.referenceNode:e}var t=n&&!(!window.MSInputMethodContext||!document.documentMode),r=n&&/MSIE 10/.test(navigator.userAgent);function g(e){return 11===e?t:10===e?r:t||r}function w(e){if(!e)return document.documentElement;for(var t=g(10)?document.body:null,n=e.offsetParent||null;n===t&&e.nextElementSibling;)n=(e=e.nextElementSibling).offsetParent;var o=n&&n.nodeName;return o&&"BODY"!==o&&"HTML"!==o?-1!==["TH","TD","TABLE"].indexOf(n.nodeName)&&"static"===y(n,"position")?w(n):n:e?e.ownerDocument.documentElement:document.documentElement}function l(e){return null!==e.parentNode?l(e.parentNode):e}function b(e,t){if(!(e&&e.nodeType&&t&&t.nodeType))return document.documentElement;var n=e.compareDocumentPosition(t)&Node.DOCUMENT_POSITION_FOLLOWING,o=n?e:t,r=n?t:e,i=document.createRange();i.setStart(o,0),i.setEnd(r,0);var a,s,f=i.commonAncestorContainer;if(e!==f&&t!==f||o.contains(r))return"BODY"===(s=(a=f).nodeName)||"HTML"!==s&&w(a.firstElementChild)!==a?w(f):f;var p=l(e);return p.host?b(p.host,t):b(e,l(t).host)}function x(e,t){var n="top"===(1<arguments.length&&void 0!==t?t:"top")?"scrollTop":"scrollLeft",o=e.nodeName;if("BODY"!==o&&"HTML"!==o)return e[n];var r=e.ownerDocument.documentElement;return(e.ownerDocument.scrollingElement||r)[n]}function d(e,t){var n="x"===t?"Left":"Top",o="Left"==n?"Right":"Bottom";return parseFloat(e["border"+n+"Width"],10)+parseFloat(e["border"+o+"Width"],10)}function s(e,t,n,o){return Math.max(t["offset"+e],t["scroll"+e],n["client"+e],n["offset"+e],n["scroll"+e],g(10)?parseInt(n["offset"+e])+parseInt(o["margin"+("Height"===e?"Top":"Left")])+parseInt(o["margin"+("Height"===e?"Bottom":"Right")]):0)}function E(e){var t=e.body,n=e.documentElement,o=g(10)&&getComputedStyle(n);return{height:s("Height",t,n,o),width:s("Width",t,n,o)}}var e=function(e,t,n){return t&&f(e.prototype,t),n&&f(e,n),e};function f(e,t){for(var n=0;n<t.length;n++){var o=t[n];o.enumerable=o.enumerable||!1,o.configurable=!0,"value"in o&&(o.writable=!0),Object.defineProperty(e,o.key,o)}}function O(e,t,n){return t in e?Object.defineProperty(e,t,{value:n,enumerable:!0,configurable:!0,writable:!0}):e[t]=n,e}var L=Object.assign||function(e){for(var t=1;t<arguments.length;t++){var n=arguments[t];for(var o in n)Object.prototype.hasOwnProperty.call(n,o)&&(e[o]=n[o])}return e};function T(e){return L({},e,{right:e.left+e.width,bottom:e.top+e.height})}function D(e){var t={};try{if(g(10)){t=e.getBoundingClientRect();var n=x(e,"top"),o=x(e,"left");t.top+=n,t.left+=o,t.bottom+=n,t.right+=o}else t=e.getBoundingClientRect()}catch(e){}var r={left:t.left,top:t.top,width:t.right-t.left,height:t.bottom-t.top},i="HTML"===e.nodeName?E(e.ownerDocument):{},a=i.width||e.clientWidth||r.width,s=i.height||e.clientHeight||r.height,f=e.offsetWidth-a,p=e.offsetHeight-s;if(f||p){var l=y(e);f-=d(l,"x"),p-=d(l,"y"),r.width-=f,r.height-=p}return T(r)}function M(e,t,n){var o=2<arguments.length&&void 0!==n&&n,r=g(10),i="HTML"===t.nodeName,a=D(e),s=D(t),f=m(e),p=y(t),l=parseFloat(p.borderTopWidth,10),d=parseFloat(p.borderLeftWidth,10);o&&i&&(s.top=Math.max(s.top,0),s.left=Math.max(s.left,0));var u=T({top:a.top-s.top-l,left:a.left-s.left-d,width:a.width,height:a.height});if(u.marginTop=0,u.marginLeft=0,!r&&i){var c=parseFloat(p.marginTop,10),h=parseFloat(p.marginLeft,10);u.top-=l-c,u.bottom-=l-c,u.left-=d-h,u.right-=d-h,u.marginTop=c,u.marginLeft=h}return(r&&!o?t.contains(f):t===f&&"BODY"!==f.nodeName)&&(u=function(e,t,n){var o=2<arguments.length&&void 0!==n&&n,r=x(t,"top"),i=x(t,"left"),a=o?-1:1;return e.top+=r*a,e.bottom+=r*a,e.left+=i*a,e.right+=i*a,e}(u,t)),u}function N(e){if(!e||!e.parentElement||g())return document.documentElement;for(var t=e.parentElement;t&&"none"===y(t,"transform");)t=t.parentElement;return t||document.documentElement}function c(e,t,n,o,r){var i=4<arguments.length&&void 0!==r&&r,a={top:0,left:0},s=i?N(e):b(e,v(t));if("viewport"===o)a=function(e,t){var n=1<arguments.length&&void 0!==t&&t,o=e.ownerDocument.documentElement,r=M(e,o),i=Math.max(o.clientWidth,window.innerWidth||0),a=Math.max(o.clientHeight,window.innerHeight||0),s=n?0:x(o),f=n?0:x(o,"left");return T({top:s-r.top+r.marginTop,left:f-r.left+r.marginLeft,width:i,height:a})}(s,i);else{var f=void 0;"scrollParent"===o?"BODY"===(f=m(h(t))).nodeName&&(f=e.ownerDocument.documentElement):f="window"===o?e.ownerDocument.documentElement:o;var p=M(f,s,i);if("HTML"!==f.nodeName||function e(t){var n=t.nodeName;if("BODY"===n||"HTML"===n)return!1;if("fixed"===y(t,"position"))return!0;var o=h(t);return!!o&&e(o)}(s))a=p;else{var l=E(e.ownerDocument),d=l.height,u=l.width;a.top+=p.top-p.marginTop,a.bottom=d+p.top,a.left+=p.left-p.marginLeft,a.right=u+p.left}}var c="number"==typeof(n=n||0);return a.left+=c?n:n.left||0,a.top+=c?n:n.top||0,a.right-=c?n:n.right||0,a.bottom-=c?n:n.bottom||0,a}function p(e,t,o,n,r,i){var a=5<arguments.length&&void 0!==i?i:0;if(-1===e.indexOf("auto"))return e;var s=c(o,n,a,r),f={top:{width:s.width,height:t.top-s.top},right:{width:s.right-t.right,height:s.height},bottom:{width:s.width,height:s.bottom-t.bottom},left:{width:t.left-s.left,height:s.height}},p=Object.keys(f).map(function(e){return L({key:e},f[e],{area:(t=f[e]).width*t.height});var t}).sort(function(e,t){return t.area-e.area}),l=p.filter(function(e){var t=e.width,n=e.height;return t>=o.clientWidth&&n>=o.clientHeight}),d=0<l.length?l[0].key:p[0].key,u=e.split("-")[1];return d+(u?"-"+u:"")}function u(e,t,n,o){var r=3<arguments.length&&void 0!==o?o:null;return M(n,r?N(t):b(t,v(n)),r)}function F(e){var t=e.ownerDocument.defaultView.getComputedStyle(e),n=parseFloat(t.marginTop||0)+parseFloat(t.marginBottom||0),o=parseFloat(t.marginLeft||0)+parseFloat(t.marginRight||0);return{width:e.offsetWidth+o,height:e.offsetHeight+n}}function k(e){var t={left:"right",right:"left",bottom:"top",top:"bottom"};return e.replace(/left|right|bottom|top/g,function(e){return t[e]})}function H(e,t,n){n=n.split("-")[0];var o=F(e),r={width:o.width,height:o.height},i=-1!==["right","left"].indexOf(n),a=i?"top":"left",s=i?"left":"top",f=i?"height":"width",p=i?"width":"height";return r[a]=t[a]+t[f]/2-o[f]/2,r[s]=n===s?t[s]-o[p]:t[k(s)],r}function C(e,t){return Array.prototype.find?e.find(t):e.filter(t)[0]}function B(e,n,t){return(void 0===t?e:e.slice(0,function(e,t,n){if(Array.prototype.findIndex)return e.findIndex(function(e){return e[t]===n});var o=C(e,function(e){return e[t]===n});return e.indexOf(o)}(e,"name",t))).forEach(function(e){e.function&&console.warn("`modifier.function` is deprecated, use `modifier.fn`!");var t=e.function||e.fn;e.enabled&&a(t)&&(n.offsets.popper=T(n.offsets.popper),n.offsets.reference=T(n.offsets.reference),n=t(n,e))}),n}function A(e,n){return e.some(function(e){var t=e.name;return e.enabled&&t===n})}function P(e){for(var t=[!1,"ms","Webkit","Moz","O"],n=e.charAt(0).toUpperCase()+e.slice(1),o=0;o<t.length;o++){var r=t[o],i=r?""+r+n:e;if(void 0!==document.body.style[i])return i}return null}function S(e){var t=e.ownerDocument;return t?t.defaultView:window}function W(e,t,n,o){n.updateBound=o,S(e).addEventListener("resize",n.updateBound,{passive:!0});var r=m(e);return function e(t,n,o,r){var i="BODY"===t.nodeName,a=i?t.ownerDocument.defaultView:t;a.addEventListener(n,o,{passive:!0}),i||e(m(a.parentNode),n,o,r),r.push(a)}(r,"scroll",n.updateBound,n.scrollParents),n.scrollElement=r,n.eventsEnabled=!0,n}function j(){var e,t;this.state.eventsEnabled&&(cancelAnimationFrame(this.scheduleUpdate),this.state=(e=this.reference,t=this.state,S(e).removeEventListener("resize",t.updateBound),t.scrollParents.forEach(function(e){e.removeEventListener("scroll",t.updateBound)}),t.updateBound=null,t.scrollParents=[],t.scrollElement=null,t.eventsEnabled=!1,t))}function I(e){return""!==e&&!isNaN(parseFloat(e))&&isFinite(e)}function R(n,o){Object.keys(o).forEach(function(e){var t="";-1!==["width","height","top","right","bottom","left"].indexOf(e)&&I(o[e])&&(t="px"),n.style[e]=o[e]+t})}function U(e,t){function n(e){return e}var o=e.offsets,r=o.popper,i=o.reference,a=Math.round,s=Math.floor,f=a(i.width),p=a(r.width),l=-1!==["left","right"].indexOf(e.placement),d=-1!==e.placement.indexOf("-"),u=t?l||d||f%2==p%2?a:s:n,c=t?a:n;return{left:u(f%2==1&&p%2==1&&!d&&t?r.left-1:r.left),top:c(r.top),bottom:c(r.bottom),right:u(r.right)}}var Y=n&&/Firefox/i.test(navigator.userAgent);function V(e,t,n){var o=C(e,function(e){return e.name===t}),r=!!o&&e.some(function(e){return e.name===n&&e.enabled&&e.order<o.order});if(!r){var i="`"+t+"`",a="`"+n+"`";console.warn(a+" modifier is required by "+i+" modifier in order to work, be sure to include it before "+i+"!")}return r}var q=["auto-start","auto","auto-end","top-start","top","top-end","right-start","right","right-end","bottom-end","bottom","bottom-start","left-end","left","left-start"],z=q.slice(3);function G(e,t){var n=1<arguments.length&&void 0!==t&&t,o=z.indexOf(e),r=z.slice(o+1).concat(z.slice(0,o));return n?r.reverse():r}var _="flip",X="clockwise",J="counterclockwise";function K(e,r,i,t){var a=[0,0],s=-1!==["right","left"].indexOf(t),n=e.split(/(\+|\-)/).map(function(e){return e.trim()}),o=n.indexOf(C(n,function(e){return-1!==e.search(/,|\s/)}));n[o]&&-1===n[o].indexOf(",")&&console.warn("Offsets separated by white space(s) are deprecated, use a comma (,) instead.");var f=/\s*,\s*|\s+/,p=-1!==o?[n.slice(0,o).concat([n[o].split(f)[0]]),[n[o].split(f)[1]].concat(n.slice(o+1))]:[n];return(p=p.map(function(e,t){var n=(1===t?!s:s)?"height":"width",o=!1;return e.reduce(function(e,t){return""===e[e.length-1]&&-1!==["+","-"].indexOf(t)?(e[e.length-1]=t,o=!0,e):o?(e[e.length-1]+=t,o=!1,e):e.concat(t)},[]).map(function(e){return function(e,t,n,o){var r=e.match(/((?:\-|\+)?\d*\.?\d*)(.*)/),i=+r[1],a=r[2];if(!i)return e;if(0!==a.indexOf("%"))return"vh"!==a&&"vw"!==a?i:("vh"===a?Math.max(document.documentElement.clientHeight,window.innerHeight||0):Math.max(document.documentElement.clientWidth,window.innerWidth||0))/100*i;var s=void 0;switch(a){case"%p":s=n;break;case"%":case"%r":default:s=o}return T(s)[t]/100*i}(e,n,r,i)})})).forEach(function(n,o){n.forEach(function(e,t){I(e)&&(a[o]+=e*("-"===n[t-1]?-1:1))})}),a}var Q={placement:"bottom",positionFixed:!1,eventsEnabled:!0,removeOnDestroy:!1,onCreate:function(){},onUpdate:function(){},modifiers:{shift:{order:100,enabled:!0,fn:function(e){var t=e.placement,n=t.split("-")[0],o=t.split("-")[1];if(o){var r=e.offsets,i=r.reference,a=r.popper,s=-1!==["bottom","top"].indexOf(n),f=s?"left":"top",p=s?"width":"height",l={start:O({},f,i[f]),end:O({},f,i[f]+i[p]-a[p])};e.offsets.popper=L({},a,l[o])}return e}},offset:{order:200,enabled:!0,fn:function(e,t){var n=t.offset,o=e.placement,r=e.offsets,i=r.popper,a=r.reference,s=o.split("-")[0],f=void 0;return f=I(+n)?[+n,0]:K(n,i,a,s),"left"===s?(i.top+=f[0],i.left-=f[1]):"right"===s?(i.top+=f[0],i.left+=f[1]):"top"===s?(i.left+=f[0],i.top-=f[1]):"bottom"===s&&(i.left+=f[0],i.top+=f[1]),e.popper=i,e},offset:0},preventOverflow:{order:300,enabled:!0,fn:function(e,o){var t=o.boundariesElement||w(e.instance.popper);e.instance.reference===t&&(t=w(t));var n=P("transform"),r=e.instance.popper.style,i=r.top,a=r.left,s=r[n];r.top="",r.left="",r[n]="";var f=c(e.instance.popper,e.instance.reference,o.padding,t,e.positionFixed);r.top=i,r.left=a,r[n]=s,o.boundaries=f;var p=o.priority,l=e.offsets.popper,d={primary:function(e){var t=l[e];return l[e]<f[e]&&!o.escapeWithReference&&(t=Math.max(l[e],f[e])),O({},e,t)},secondary:function(e){var t="right"===e?"left":"top",n=l[t];return l[e]>f[e]&&!o.escapeWithReference&&(n=Math.min(l[t],f[e]-("right"===e?l.width:l.height))),O({},t,n)}};return p.forEach(function(e){var t=-1!==["left","top"].indexOf(e)?"primary":"secondary";l=L({},l,d[t](e))}),e.offsets.popper=l,e},priority:["left","right","top","bottom"],padding:5,boundariesElement:"scrollParent"},keepTogether:{order:400,enabled:!0,fn:function(e){var t=e.offsets,n=t.popper,o=t.reference,r=e.placement.split("-")[0],i=Math.floor,a=-1!==["top","bottom"].indexOf(r),s=a?"right":"bottom",f=a?"left":"top",p=a?"width":"height";return n[s]<i(o[f])&&(e.offsets.popper[f]=i(o[f])-n[p]),n[f]>i(o[s])&&(e.offsets.popper[f]=i(o[s])),e}},arrow:{order:500,enabled:!0,fn:function(e,t){var n;if(!V(e.instance.modifiers,"arrow","keepTogether"))return e;var o=t.element;if("string"==typeof o){if(!(o=e.instance.popper.querySelector(o)))return e}else if(!e.instance.popper.contains(o))return console.warn("WARNING: `arrow.element` must be child of its popper element!"),e;var r=e.placement.split("-")[0],i=e.offsets,a=i.popper,s=i.reference,f=-1!==["left","right"].indexOf(r),p=f?"height":"width",l=f?"Top":"Left",d=l.toLowerCase(),u=f?"left":"top",c=f?"bottom":"right",h=F(o)[p];s[c]-h<a[d]&&(e.offsets.popper[d]-=a[d]-(s[c]-h)),s[d]+h>a[c]&&(e.offsets.popper[d]+=s[d]+h-a[c]),e.offsets.popper=T(e.offsets.popper);var m=s[d]+s[p]/2-h/2,v=y(e.instance.popper),g=parseFloat(v["margin"+l],10),b=parseFloat(v["border"+l+"Width"],10),w=m-e.offsets.popper[d]-g-b;return w=Math.max(Math.min(a[p]-h,w),0),e.arrowElement=o,e.offsets.arrow=(O(n={},d,Math.round(w)),O(n,u,""),n),e},element:"[x-arrow]"},flip:{order:600,enabled:!0,fn:function(v,g){if(A(v.instance.modifiers,"inner"))return v;if(v.flipped&&v.placement===v.originalPlacement)return v;var b=c(v.instance.popper,v.instance.reference,g.padding,g.boundariesElement,v.positionFixed),w=v.placement.split("-")[0],y=k(w),x=v.placement.split("-")[1]||"",E=[];switch(g.behavior){case _:E=[w,y];break;case X:E=G(w);break;case J:E=G(w,!0);break;default:E=g.behavior}return E.forEach(function(e,t){if(w!==e||E.length===t+1)return v;w=v.placement.split("-")[0],y=k(w);var n,o=v.offsets.popper,r=v.offsets.reference,i=Math.floor,a="left"===w&&i(o.right)>i(r.left)||"right"===w&&i(o.left)<i(r.right)||"top"===w&&i(o.bottom)>i(r.top)||"bottom"===w&&i(o.top)<i(r.bottom),s=i(o.left)<i(b.left),f=i(o.right)>i(b.right),p=i(o.top)<i(b.top),l=i(o.bottom)>i(b.bottom),d="left"===w&&s||"right"===w&&f||"top"===w&&p||"bottom"===w&&l,u=-1!==["top","bottom"].indexOf(w),c=!!g.flipVariations&&(u&&"start"===x&&s||u&&"end"===x&&f||!u&&"start"===x&&p||!u&&"end"===x&&l),h=!!g.flipVariationsByContent&&(u&&"start"===x&&f||u&&"end"===x&&s||!u&&"start"===x&&l||!u&&"end"===x&&p),m=c||h;(a||d||m)&&(v.flipped=!0,(a||d)&&(w=E[t+1]),m&&(x="end"===(n=x)?"start":"start"===n?"end":n),v.placement=w+(x?"-"+x:""),v.offsets.popper=L({},v.offsets.popper,H(v.instance.popper,v.offsets.reference,v.placement)),v=B(v.instance.modifiers,v,"flip"))}),v},behavior:"flip",padding:5,boundariesElement:"viewport",flipVariations:!1,flipVariationsByContent:!1},inner:{order:700,enabled:!1,fn:function(e){var t=e.placement,n=t.split("-")[0],o=e.offsets,r=o.popper,i=o.reference,a=-1!==["left","right"].indexOf(n),s=-1===["top","left"].indexOf(n);return r[a?"left":"top"]=i[n]-(s?r[a?"width":"height"]:0),e.placement=k(t),e.offsets.popper=T(r),e}},hide:{order:800,enabled:!0,fn:function(e){if(!V(e.instance.modifiers,"hide","preventOverflow"))return e;var t=e.offsets.reference,n=C(e.instance.modifiers,function(e){return"preventOverflow"===e.name}).boundaries;if(t.bottom<n.top||t.left>n.right||t.top>n.bottom||t.right<n.left){if(!0===e.hide)return e;e.hide=!0,e.attributes["x-out-of-boundaries"]=""}else{if(!1===e.hide)return e;e.hide=!1,e.attributes["x-out-of-boundaries"]=!1}return e}},computeStyle:{order:850,enabled:!0,fn:function(e,t){var n=t.x,o=t.y,r=e.offsets.popper,i=C(e.instance.modifiers,function(e){return"applyStyle"===e.name}).gpuAcceleration;void 0!==i&&console.warn("WARNING: `gpuAcceleration` option moved to `computeStyle` modifier and will not be supported in future versions of Popper.js!");var a=void 0!==i?i:t.gpuAcceleration,s=w(e.instance.popper),f=D(s),p={position:r.position},l=U(e,window.devicePixelRatio<2||!Y),d="bottom"===n?"top":"bottom",u="right"===o?"left":"right",c=P("transform"),h=void 0,m=void 0;if(m="bottom"==d?"HTML"===s.nodeName?-s.clientHeight+l.bottom:-f.height+l.bottom:l.top,h="right"==u?"HTML"===s.nodeName?-s.clientWidth+l.right:-f.width+l.right:l.left,a&&c)p[c]="translate3d("+h+"px, "+m+"px, 0)",p[d]=0,p[u]=0,p.willChange="transform";else{var v="bottom"==d?-1:1,g="right"==u?-1:1;p[d]=m*v,p[u]=h*g,p.willChange=d+", "+u}var b={"x-placement":e.placement};return e.attributes=L({},b,e.attributes),e.styles=L({},p,e.styles),e.arrowStyles=L({},e.offsets.arrow,e.arrowStyles),e},gpuAcceleration:!0,x:"bottom",y:"right"},applyStyle:{order:900,enabled:!0,fn:function(e){var t,n;return R(e.instance.popper,e.styles),t=e.instance.popper,n=e.attributes,Object.keys(n).forEach(function(e){!1!==n[e]?t.setAttribute(e,n[e]):t.removeAttribute(e)}),e.arrowElement&&Object.keys(e.arrowStyles).length&&R(e.arrowElement,e.arrowStyles),e},onLoad:function(e,t,n,o,r){var i=u(r,t,e,n.positionFixed),a=p(n.placement,i,t,e,n.modifiers.flip.boundariesElement,n.modifiers.flip.padding);return t.setAttribute("x-placement",a),R(t,{position:n.positionFixed?"fixed":"absolute"}),n},gpuAcceleration:void 0}}},Z=(e($,[{key:"update",value:function(){return function(){if(!this.state.isDestroyed){var e={instance:this,styles:{},arrowStyles:{},attributes:{},flipped:!1,offsets:{}};e.offsets.reference=u(this.state,this.popper,this.reference,this.options.positionFixed),e.placement=p(this.options.placement,e.offsets.reference,this.popper,this.reference,this.options.modifiers.flip.boundariesElement,this.options.modifiers.flip.padding),e.originalPlacement=e.placement,e.positionFixed=this.options.positionFixed,e.offsets.popper=H(this.popper,e.offsets.reference,e.placement),e.offsets.popper.position=this.options.positionFixed?"fixed":"absolute",e=B(this.modifiers,e),this.state.isCreated?this.options.onUpdate(e):(this.state.isCreated=!0,this.options.onCreate(e))}}.call(this)}},{key:"destroy",value:function(){return function(){return this.state.isDestroyed=!0,A(this.modifiers,"applyStyle")&&(this.popper.removeAttribute("x-placement"),this.popper.style.position="",this.popper.style.top="",this.popper.style.left="",this.popper.style.right="",this.popper.style.bottom="",this.popper.style.willChange="",this.popper.style[P("transform")]=""),this.disableEventListeners(),this.options.removeOnDestroy&&this.popper.parentNode.removeChild(this.popper),this}.call(this)}},{key:"enableEventListeners",value:function(){return function(){this.state.eventsEnabled||(this.state=W(this.reference,this.options,this.state,this.scheduleUpdate))}.call(this)}},{key:"disableEventListeners",value:function(){return j.call(this)}}]),$);function $(e,t){var n=this,o=2<arguments.length&&void 0!==arguments[2]?arguments[2]:{};!function(e,t){if(!(e instanceof t))throw new TypeError("Cannot call a class as a function")}(this,$),this.scheduleUpdate=function(){return requestAnimationFrame(n.update)},this.update=i(this.update.bind(this)),this.options=L({},$.Defaults,o),this.state={isDestroyed:!1,isCreated:!1,scrollParents:[]},this.reference=e&&e.jquery?e[0]:e,this.popper=t&&t.jquery?t[0]:t,this.options.modifiers={},Object.keys(L({},$.Defaults.modifiers,o.modifiers)).forEach(function(e){n.options.modifiers[e]=L({},$.Defaults.modifiers[e]||{},o.modifiers?o.modifiers[e]:{})}),this.modifiers=Object.keys(this.options.modifiers).map(function(e){return L({name:e},n.options.modifiers[e])}).sort(function(e,t){return e.order-t.order}),this.modifiers.forEach(function(e){e.enabled&&a(e.onLoad)&&e.onLoad(n.reference,n.popper,n.options,e,n.state)}),this.update();var r=this.options.eventsEnabled;r&&this.enableEventListeners(),this.state.eventsEnabled=r}return Z.Utils=("undefined"!=typeof window?window:global).PopperUtils,Z.placements=q,Z.Defaults=Q,Z});