function showStickyNoticeToast() {
    $().toastmessage('showToast', {
        text: 'Попрыгайте на месте  ;)',
        sticky: true,
        position: 'middle-center',
        type: 'notice',
        closeText: '',
        close: function () { console.log("toast is closed ..."); }
    });
}

function showStickyWarningToast() {
    $().toastmessage('showToast', {
        text: 'Сервис не поддерживает вашу версию браузера.',
        sticky: true,
        position: 'middle-center',
        type: 'warning',
        closeText: '',
        close: function () {
            console.log("toast is closed ...");
        }
    });
}