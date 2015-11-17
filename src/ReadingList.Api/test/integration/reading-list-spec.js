// spec.js
describe('Protractor Demo App', function () {
    it('should have a title', function() {
        browser.get('http://localhost:5000/app/index.html');

        expect(browser.getTitle()).toEqual('Reading List');

    });
    it('should have the right number of items', function () {
        browser.get('http://localhost:5000/app/index.html');

        var ids = element.all(by.binding('item.Id'));

        expect(ids.count()).toEqual(8);
    });
});