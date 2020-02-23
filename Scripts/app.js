var ViewModel = function () {
    var self = this;
    self.zords = ko.observableArray();
    self.error = ko.observable();

    var zordsUri = 'api/zords';

    function ajaxHelper(uri, method, data) {
        self.error('');
        return $.ajax({
            type: method,
            url: uri,
            dataType: 'json',
            contentType: 'application/json',
            data: data ? JSON.stringify(data) : null
        }).fail(function (jqXHR, textStatus, errorThrown) {
            self.error(errorThrown);
        });
    }
    function getAllZords() {
        ajaxHelper(zordsUri, 'GET').done(function (data) {
            self.zords(data);
        });
    }
    //
    getAllZords();
    self.detail = ko.observable();

    self.getZordsDetail = function (item) {
        ajaxHelper(zordsUri + item.Id, 'GET').done(function (data) {
            self.detail(data);
        });
        self.prods = ko.observableArray();
        self.newZord = {
            Prod: ko.observable(),
            Name: ko.observable(),
            Year: ko.observable()
        }
        var prodsUri = '/apiprods/';
        function getProds() {
            ajaxHelper(prodsUri, 'GET').done(function (data) {
                self.prods(data);
            });
        }
        self.addZords = function (formElement) {
            var zord = {
                ProdId=self.newZord.Prod().Id,
                Name: self.newZord.Name(),
                Year: self.newZord.Year()
            };
            ajaxHelper(zordsUri, 'POST', zord).done(function (item) {
                self.zords.push(item);
            });
        }
        getProds();
    }
};
ko.applyBindings(new ViewModel());