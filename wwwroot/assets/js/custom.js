window.addEventListener('load', function () {

     

    setTimeout(function () {

        console.log('cargadop')
        var btn = document.querySelector('.sidenav-toggler .sidenav-toggler-inner');
        if (btn === null || typeof btn === "undefined") {
            console.log('btn no existe')
        } else { console.log('btnEXISTE') }
        // click hamburger => show /hide left navbar
        document.querySelector('.sidenav-toggler .sidenav-toggler-inner').addEventListener('click', function (event) {
            console.log('clickeado')
            $('body').toggleClass('g-sidenav-show g-sidenav-hidden');
        }, true);

        var fixedRightNavbarButton = document.querySelector('.fixed-plugin-button-nav');
        var fixedRightNavbar = document.querySelector('.fixed-plugin');


        //onclick top options button => show right fixed navbar
        fixedRightNavbarButton.addEventListener('click', function (event) {
            $(fixedRightNavbar).addClass('show');
        }, true);
        
        //onclick bottom options button => show right fixed navbar
        document.querySelector('.fixed-plugin .fixed-plugin-button').addEventListener('click', function (event) {
            $(fixedRightNavbar).addClass('show');
        });

        //close right navbar button
        $('.fixed-plugin-close-button').on('click', function () {
            $(fixedRightNavbar).removeClass('show');
        });


        document.addEventListener('click', function (event) {

            //show/hide notification menu
            var dropdownNotificationButton = document.querySelector('li.nav-item.dropdown.pe-2 > a.nav-link');
            if (dropdownNotificationButton.contains(event.target) && event.target.parentNode == dropdownNotificationButton) {
                $('#notificationDropDown').toggleClass('show');
            } else {
                $('#notificationDropDown').removeClass('show');
            }

            //hide the right navigation bar if click anywhere else
            if (!fixedRightNavbar.contains(event.target) && event.target.id !== 'fixedRightNavbarButton2' && event.target.id !== 'closeRightNavbar' && event.target.id !== 'fixedRightNavbarButton') {
                $(fixedRightNavbar).removeClass('show')
            }

        },true );


        //evento onclick modal "save changes button"
        var btnModalSave = document.querySelector('.modal-footer .btn:nth-of-type(2)');
        if (btnModalSave !== null) {
            btnModalSave.addEventListener('click', function () {
                $(document.querySelector('.modal-footer .btn:nth-of-type(1)')).trigger('click');
            });
        }


        // notification toast show/hide
        $('.showtoast').on('click', function () {
            $('.toast.bg-gradient-dark').addClass('show');
        });
        $('.closetoast').on('click', function () {
            $('.toast.bg-gradient-dark').removeClass('show')
        });

    }, 400)

})
