﻿////function render_recaptcha(dotNetObj, selector, sitekey) {
////    return grecaptcha.render(selector, {
////        'sitekey': sitekey,
////        'callback': (response) => { dotNetObj.invokeMethodAsync('CallbackOnSuccess', response); },
////        'expired-callback': () => { dotNetObj.invokeMethodAsync('CallbackOnExpired'); }
////    });
////};

////function getResponse(widgetId) {
////    return grecaptcha.getResponse(widgetId);
////}
window.getCaptcha = async function () {
    await grecaptcha.ready(function () { });

    const token = await grecaptcha.execute('XXX', { action: 'formSubmission' })

    return token;
}