var subHub;
$(function () {
    subHub = $.connection.subHub;

    subHub.client.subscribe = function () {
        afterSubscribe(); 
        $('.follow button').empty();
        $('.follow button').append('<i class="fas fa-users"></i> Following');
    }

    subHub.client.unsubscribe = function () {
        afterUnsubscribe(); 
        $('.follow button').empty();
        $('.follow button').append('<i class="fas fa-users"></i> Follow');
    }

    subHub.client.redirectToLogin = function () {
        window.location.href = "/login";
    }

    $.connection.hub.start().done();
});
function subscribe(toId) {
    subHub.server.subscribe(toId);
}