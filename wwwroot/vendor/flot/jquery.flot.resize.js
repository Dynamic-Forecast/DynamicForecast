!function(o,s,u){var h,m=[],l=o.resize=o.extend(o.resize,{}),c=!1,i="setTimeout",d="resize",f=d+"-special-event",g="pendingDelay",n="activeDelay",a="throttleWindow";function v(e){!0===c&&(c=e||1);for(var t=m.length-1;0<=t;t--){var i=o(m[t]);if(i[0]==s||i.is(":visible")){var n=i.width(),a=i.height(),r=i.data(f);!r||n===r.w&&a===r.h||(i.trigger(d,[r.w=n,r.h=a]),c=e||!0)}else(r=i.data(f)).w=0,r.h=0}null!==h&&(c&&(null==e||e-c<1e3)?h=s.requestAnimationFrame(v):(h=setTimeout(v,l[g]),c=!1))}l[g]=200,l[n]=20,l[a]=!0,o.event.special[d]={setup:function(){if(!l[a]&&this[i])return!1;var e=o(this);m.push(this),e.data(f,{w:e.width(),h:e.height()}),1===m.length&&(h=u,v())},teardown:function(){if(!l[a]&&this[i])return!1;for(var e=o(this),t=m.length-1;0<=t;t--)if(m[t]==this){m.splice(t,1);break}e.removeData(f),m.length||((c?cancelAnimationFrame:clearTimeout)(h),h=null)},add:function(e){if(!l[a]&&this[i])return!1;var r;function t(e,t,i){var n=o(this),a=n.data(f)||{};a.w=t!==u?t:n.width(),a.h=i!==u?i:n.height(),r.apply(this,arguments)}if(o.isFunction(e))return r=e,t;r=e.handler,e.handler=t}},s.requestAnimationFrame||(s.requestAnimationFrame=s.webkitRequestAnimationFrame||s.mozRequestAnimationFrame||s.oRequestAnimationFrame||s.msRequestAnimationFrame||function(e,t){return s.setTimeout(function(){e((new Date).getTime())},l[n])}),s.cancelAnimationFrame||(s.cancelAnimationFrame=s.webkitCancelRequestAnimationFrame||s.mozCancelRequestAnimationFrame||s.oCancelRequestAnimationFrame||s.msCancelRequestAnimationFrame||clearTimeout)}(jQuery,this),jQuery.plot.plugins.push({init:function(t){function i(){var e=t.getPlaceholder();0!=e.width()&&0!=e.height()&&(t.resize(),t.setupGrid(),t.draw())}t.hooks.bindEvents.push(function(e,t){e.getPlaceholder().resize(i)}),t.hooks.shutdown.push(function(e,t){e.getPlaceholder().unbind("resize",i)})},options:{},name:"resize",version:"1.0"});