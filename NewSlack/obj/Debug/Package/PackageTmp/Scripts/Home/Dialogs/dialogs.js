$(function () {
    $('.remove_dialog img').hover(function () {
        $(this).attr('src', 'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAACgAAAAoCAYAAACM/rhtAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAARRSURBVFhH7VdtiFRVGB7TCjOjAkERtLKgL8rwKwxcwty9H2v+yM1fUT8ispRtnbnn7io2iP0o8kf6ow8q0XTnnDt/kggR/GFsiBr9iCCICNNADFMsWkXXcn2ec9+ZnXudGafdraaaBy53znPe857nvOc97z2TaeF/h7b8gUnys/ngrN17oxfoU3i2d3UVJwrdXPCU/tBTZtgN9I6mFElRFNcSOVb8Z0Q6fdE0VxWWuYF5Hvmb85V5xcvp5xxlHl2W/WiKmP11qCYSQu5zlXkd72/I13pgfxE2n7lKr2nviW4Xl+OPSpF4vsekl8silDmMvm4vW1jk9/bf5WfNg05gPPS9haj+NGKnz5PzcsXp4nb8sLInmpwSyegcd3K6XUyqojP/yU0QthF19VKlUAh/sy1fvFnMRo98Pn8dIqQQsTNeaJYkRerdbItpXTiBXgqRv0HYHxj/lR0fmB/8nHlCTP48OkJ9B8QNxNEyB52w8EhaJN+NivRCvcKmRmD6IfaZePv1ZUR027wX3rtezBqDE0RtjBoEXISDdYwk2hvQPtexfteMUYtUeidFOdnofuTrbRD5aRxN/XnDuckSEQszJ+BsMTm71cg5cN9lMsMTyKVF2gmVdv3e4rxagpdnozspEFF8O2aGJ0BcyByl/86+/ntjvgZg1C0r+pqREjrjh+ZhKwT5KJRFpUg8Q/K2J7tWRGB/JF7oCOTUD6LvtBsU5gudBFb1EldHB+ma5avo2XjyOKKVSImsfIyYJIBt3cp+VgahLJxeswDCf8YcZ5xc4SGhYzD5rTis/Mlgz1Shy4DTLJ2yzgmVQFWRgf5FuhOwW4p+lKg5QpUhO/UrD1Di4PDEYuAproBGQpeB3FpDpx2q8IBQZfhB/z0sF2mRGHNSTBLAPK9amyopgPFIMXuy9ws1AltGsGqG2M2Zx4S2QPspOmWuCFVGXCv1/qtE4mSyLWZlIDofoH8ofXMHl7fj8GmsWcSxqrkMLyY876jCSqEzPDAyeItQCdgLQ6DfLZ3gkki+0yLh+yh2akCaGW4l/JYuyfv49ZGu6mjvje7GgG/xoBzoN0orxeAjFH9NB0AtkYyy5VBb2e4Mi7MwxyFZ/Ptd+eIN5K+JFd0f34pTvTd2ZgaY0J6KnhbnG8WsLtIi+X/HLlLps9xCpg3ap9E/5IaFl2VY4+AEWB1OnL7ALacw5hXev0P842JWF5Ui412hWPMaHvsFweKPsYKI+ejA6xMm+TKexAzGk+hzTmiWi0ldJCKJxdnF4tIAwe84a3fdImZjA08qizUmOi5CGRHcCfVOlhkxS8DuAC4IrAjJSJpB+FooZuML5pGj9Gps9ReVQlngEZWt4AP87sNvlpIfS4LGcgsaNfhhh5hNEHMAwlA/S4LLW3kUfXvcIHrR74lmcszfLrIS/H7zU+hld8+uVy7+UZGNoiVyvPDvE4lDJ3RzwYpUejOErhKqhRaaBJnMFdh/dGUXUkBYAAAAAElFTkSuQmCC');
    }, function () {
        $(this).attr('src', 'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAACgAAAAoCAYAAACM/rhtAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAPnSURBVFhH7ZdfaJVlHMdPlkalkcEgCSqnwVJpRmXSYBFNQSG8UOoq5kUX/oNimSNkxdCLRC+qiwpa0SDBSy9EBC82NsJNdjHG/p415wbDYRsZueH+tNPn+/Y9x/O+nHOc27RTnS98eZ7n+/ye3/N9n/d5nvecWAH/OzQ0NDzkav4hHo8/3Nvbex3+kEgkHrScX+jr6/seJjD5Y16alCmZK5hcLP4zJjlYRXAb/e8Td5jyQ8q9aFvb29sfc9i9QyaT1F+An9Pukp6DU8Q0Uh7q7Ox80imXHhGTv8C5pAnqLT09PR9Qfw0Wd3d3b6LcCb+gb1QxIvVJaRh9ymmXDsPDw4+km/SEQ7zG7Q7JiJGRkUeJqyF+Jm3cJDyJ0ZUOWzgwtYxkR0g8DssjJn9S26E5wZi3iP+D8k/Y7vGDsMIhdw9e23Mka3Kyn1mtlyhDJlXehcldcA6ehu/BUbe/amtrW+6w+QEzb2BAq6ZNXqWVpH6U+gR7bM0iTNbDOfJv6OjoWE39nHLA5nnvTYL3wik4wiq+Lk0GSTaEFqf+gLWQSU+4gzEvZzNM/1qoA/a12spFu5r2DOUQLAkCs8GnUU/UoZWyLNOl0klwxFKAdJNw2qXiWrKtCH2txMTdDEBbp/4mHMPDK5bDYOABqD3RGr2zGFgJE8kVTUfEZDrPOCQEYr9Uv24GSwHQX0X/FY5Tf9Hy30AsR5Q53WmrLKdA/0dKCosthZDJJO0b7g4BXa80wT5cZykF9FL6f4ejoYODKZ3Y6wToCUotp4B2SEn7+/s3Wkqhq6vrefoqMpi85pAQiPlU/Zm2gLYY/Vqoi5ZuQ9cIHTcYrCUusxyAvt2edKelFDCmA3SRvqjJZrUdlgJ6HZyO/nJn3GcaR9mY9RInYDMBup8meZo9lmM6MB58ylIIPEARfd8yJjjB1AOTKqMm0a+gN7kZ06tEC34kwwv6+rgrM3iN6wkkR3AdnEg+Ke1WOHrHBCCbSdoV1qrUHhgYeIb6JWnwO1ZuhfQ7YnBw8AkGnHeyJm1oynfcrnFYTkRNkkP/d/SQv+kVetuMwWm0gx42f3iCangL6gOvj34z5Syv8k2H5US6SdirkvZxmPyCXIXlDl8Y/POpzQl1mWqSCVbgbYfkRGQlZ6EeVj8aviHH4w5bHJhE3+JKkupzJ6OaTNdBva4Zh4VgY/qBUBZZST3kFoctLbyP9jPBZU+WNNoC9YX4GH4C62gPO0aGFvwraMFgkhImq4UN1HV/BoZtYJbyCjzLQ+2jfFpj7rvJdOj7zcTFvPJnc10X/6jJ+aJgcqnwrzMJay3nF2zyGHzXUgEF5Alisb8AyFOWsnGikCAAAAAASUVORK5CYII=');
        });

    $('.remove_dialog img').click(function () {

        var hide = confirm('Hide this dialog?');

        if (hide) {
            var dialogDiv = $(this).parent().parent();
            var dialogId = $(this).parent().siblings('.dialogId').attr('id');

            $.ajax({
                type: 'GET',
                url: '/dialog/hide',
                data: { dialogId },
                success: function (isRemoved) {
                    if (isRemoved) {
                        //remove dialog
                        dialogDiv.remove();
                    }
                },
                error: function (e) {
                    alert(e.statusText);
                }
            });
        }
    });

    $('.dialog_content').click(function () {
        var dialogId = $(this).siblings('.dialogId').attr('id');
        window.location.href = "/dialog/" + dialogId;
    });
});
