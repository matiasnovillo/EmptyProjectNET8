window.StorageFunctions = {
    setItem: function (key, value) {
        localStorage.setItem(key, value);
    },
    getItem: function (key) {
        return localStorage.getItem(key);
    },
    removeItem: function (key) {
        localStorage.removeItem(key);
    },
    setSessionItem: function (key, value) {
        sessionStorage.setItem(key, value);
    },
    getSessionItem: function (key) {
        return sessionStorage.getItem(key);
    },
    removeSessionItem: function (key) {
        sessionStorage.removeItem(key);
    }
};