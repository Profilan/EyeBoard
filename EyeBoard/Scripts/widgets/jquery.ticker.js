$(function () {
    $.widget("custom.ticker", {
        options: {
            vector: -1,
            fontSize: 30,
            fontColor: "#000",
            fontFamily: "Helvetica",
            framerate: 60,
            canvasId: "ticker-canvas",
            url: "/api/notification",
            space: 40,
            stage: null,
        },
        _create: function () {

            this.canvas = $("<canvas></canvas>").attr("id", this.options.canvasId)
                .attr("width", this.element.width).attr("height", this.element.height)
                .appendTo(this.element);

            this.canvasWidth = this.element.width();
            this.canvasHeight = this.element.height();

            this.options.stage = new createjs.Stage(this.options.canvasId);

            this.refresh();
        },
        _setOptions: function () {
            this._superApply(arguments);
            this.refresh;
        },
        refresh: function () {
            var xOffset = 0;

            $.get(this.options.url, function (data) {
                createjs.Ticker.off("tick");
                this.options.stage.removeAllChildren();
                var container = new createjs.Container();
                stage.addChild(container);

                $.each(data, function (idx, value) {
                    var text = new createjs.Text(value.Title, this.options.fontSize + "px " + this.options.fontFamily, this.options.fontColor);
                    text.x = xOffset;
                    text.y = (canvasHeight - this.options.fontSize) / 2;
                    container.addChild(text);

                    xOffset += space + text.getMeasuredWidth();

                    createjs.Ticker.on("tick", function () {
                        container.x += this.options.vector;
                        if (container.x < 0 - xOffset) { container.x = canvasWidth }
                        this.options.stage.update();
                    });

                    createjs.Ticker.framerate = this.options.framerate;
                });
            });
        }
    });
});

