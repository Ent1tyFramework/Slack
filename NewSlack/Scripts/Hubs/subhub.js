var subHub;
$(function () {
    subHub = $.connection.subHub;

    subHub.client.subscribe = function () {
        $('#follow').text('Unsubscribe');
    }

    subHub.client.unsubscribe = function () {
        $('#follow').text('Subscribe');
    }

    subHub.client.redirectToLogin = function () {
        window.location.href = "/login";
    }

    $.connection.hub.start().done();
});
function subscribe(toId) {
    subHub.server.subscribe(toId);
}