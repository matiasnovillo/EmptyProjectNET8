document.addEventListener('DOMContentLoaded', function () {
    let functionInterval;

    function verificarCargadoYEjecutar() {

        var body = document.getElementsByTagName('body')[0];
            clearInterval(functionInterval);

            document.addEventListener('click', function (event) {

                var fixedRightNavbar = document.querySelector('.fixed-plugin');

                // click hamburger => show /hide left navbar
                if (event.target.matches('.sidenav-toggler-inner') || event.target.matches('.sidenav-toggler-line') ) {
                    if (!body.classList.contains('g-sidenav-hidden')) {
                        body.classList.add('g-sidenav-hidden');
                        body.classList.remove('g-sidenav-show');
                    } else {
                        body.classList.remove('g-sidenav-hidden');
                        body.classList.add('g-sidenav-show');
                    }
                }

                // icono de opciones engranaje de arriba
                if (event.target.matches('.fixed-plugin-button-nav')) {
                    document.querySelector('.fixed-plugin').classList.add('show')
                }

                // icono de opciones engranaje de abajo
                if (event.target.matches('.fixed-plugin-button')) {
                    document.querySelector('.fixed-plugin').classList.add('show')
                }

                // close right navbar button
                if (event.target.matches('.fixed-plugin-close-button') || event.target.closest('.fixed-plugin-close-button') ) {
                    document.querySelector('.fixed-plugin').classList.remove('show')
                }


                // show/hide notification menu
                var dropdownNotificationButton = document.querySelector('li.nav-item.dropdown.pe-2 > a.nav-link');
                if (dropdownNotificationButton && dropdownNotificationButton.contains(event.target) && event.target.parentNode == dropdownNotificationButton) {
                    $('#notificationDropDown').toggleClass('show');
                } else {
                    $('#notificationDropDown').removeClass('show');
                }

                // hide the right navigation bar if click anywhere else
                if (fixedRightNavbar && !fixedRightNavbar.contains(event.target) && event.target.id !== 'fixedRightNavbarButton2' && event.target.id !== 'closeRightNavbar' && event.target.id !== 'fixedRightNavbarButton') {
                    $(fixedRightNavbar).removeClass('show');
                }


                // onclick modal btn "save changes"
                var btnModalSave = document.querySelector('.modal-footer .btn:nth-of-type(2)');
                if (event.target === btnModalSave) {
                    $(document.querySelector('.modal-footer .btn:nth-of-type(1)')).trigger('click');
                }


                // notification toast show/hide
                $(".showtoast").on("click", function () {
                    $(this).next().addClass("show");
                });

                if (event.target.matches('.closetoast')) {
                    $('.toast.bg-gradient-dark').removeClass('show');
                }

                //if (event.target.matches('.showtoast')) {
                //    $('.toast.bg-gradient-dark').addClass('show');
                //}

            }, false);
    }

    // Verificar si está cargado boton y ejecutar cada 0.5 segundos
    functionInterval = setInterval(verificarCargadoYEjecutar, 500);
});
