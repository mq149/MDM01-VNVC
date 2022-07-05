$(function () {
    const VNVC_REGISTRATION_LOCAL_STORAGE_KEY = "vnvc:registrationId";
    let registrationId = localStorage.getItem(
        VNVC_REGISTRATION_LOCAL_STORAGE_KEY
    );
    $("b.registration-id").html(registrationId);
});
