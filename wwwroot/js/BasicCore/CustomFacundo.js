document.addEventListener('DOMContentLoaded', function () {
    let functionInterval;

    function verificarCargadoYEjecutar() {
        var body = document.getElementsByTagName('body')[0];

        // Si encuentra el botón de hamburguesa y los eventos no están asignados
        if (hamburgerIcon !== null && typeof hamburgerIcon !== "undefined" && !body.classList.contains('events-assigned')) {
       
            clearInterval(functionInterval);
            body.classList.add('events-assigned');
            // click hamburger => show /hide left navbar
            document.addEventListener('click', function (event) {
                if (event.target.matches('.sidenav-toggler-inner') || event.target.matches('.sidenav-toggler-line') ) {
                    if (!body.classList.contains('g-sidenav-hidden')) {
                        body.classList.add('g-sidenav-hidden');
                        body.classList.remove('g-sidenav-show');
                    } else {
                        body.classList.remove('g-sidenav-hidden');
                        body.classList.add('g-sidenav-show');
                    }
                }
            }, false);

            var fixedRightNavbarButton = document.querySelector('.fixed-plugin-button-nav');
            var fixedRightNavbarButton2 = document.querySelector('.fixed-plugin .fixed-plugin-button');
            var fixedRightNavbar = document.querySelector('.fixed-plugin');


            document.addEventListener('click', function (event) {
                if (event.target.matches('.fixed-plugin-button-nav')) {
                    document.querySelector('.fixed-plugin').classList.add('show')
                }
            }, false);

            // onclick top options button => show right fixed navbar
            if (fixedRightNavbarButton) {
                fixedRightNavbarButton.addEventListener('click', function (event) {
                    $(fixedRightNavbar).addClass('show');
                }, true);
            }

            // onclick bottom options button => show right fixed navbar
            if (fixedRightNavbarButton2) {
                fixedRightNavbarButton2.addEventListener('click', function (event) {
                    $(fixedRightNavbar).addClass('show');
                });
            }

            // close right navbar button
            $('.fixed-plugin-close-button').on('click', function () {
                $(fixedRightNavbar).removeClass('show');
            });

            document.addEventListener('click', function (event) {
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
            }, true);

            // evento onclick modal "save changes button"
            var btnModalSave = document.querySelector('.modal-footer .btn:nth-of-type(2)');
            if (btnModalSave) {
                btnModalSave.addEventListener('click', function () {
                    $(document.querySelector('.modal-footer .btn:nth-of-type(1)')).trigger('click');
                });
            }

            // notification toast show/hide
            $('.showtoast').on('click', function () {
                $('.toast.bg-gradient-dark').addClass('show');
            });
            $('.closetoast').on('click', function () {
                $('.toast.bg-gradient-dark').removeClass('show');
            });


        } else {
            console.log('aun no lo encuentra')
        }
    }

    // Verificar si está cargado boton y ejecutar cada 0.5 segundos
    functionInterval = setInterval(verificarCargadoYEjecutar, 500);
});
