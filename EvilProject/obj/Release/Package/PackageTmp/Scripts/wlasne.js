  

    $(function () {
        $("input[type=submit]")
            .button();
    });


    $(function () {
        $("a.editlinksmall").button({
            icons: {
                primary: "ui-icon-pencil"
            },
            text: false
        });

        $("a.deletelinksmall").button({
            icons: {
                primary: "ui-icon-circle-close"
            },
            text: false
        });
    });