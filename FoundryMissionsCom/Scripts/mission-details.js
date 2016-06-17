$(window).load(function () {

    // from jspScrollable
    $(".mission-images").jScrollPane();
    // from jspScrollable
    $('.mission-images').each(
		function () {
		    $(this).jScrollPane(
				{
				    showArrows: $(this).is('.arrow')
				}
			);
		    var api = $(this).data('jsp');
		    var throttleTimeout;
		    $(window).bind(
				'resize',
				function () {
				    if (!throttleTimeout) {
				        throttleTimeout = setTimeout(
							function () {
							    api.reinitialise();
							    throttleTimeout = null;
							},
							50
						);
				    }
				}
			);
		}
	)
});