//Show/Hide left side-nav
window.JsFunctions = {
    ShowOrHideSideNav: function (BoolAsString) {

        var body = document.getElementsByTagName('body')[0];

        if (BoolAsString === "true") {
            body.classList.remove('g-sidenav-hidden');
            body.classList.add('g-sidenav-show');
        }
        else {
            body.classList.add('g-sidenav-hidden');
            body.classList.remove('g-sidenav-show');
        }
    }
};
