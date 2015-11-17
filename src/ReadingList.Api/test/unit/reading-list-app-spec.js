describe('readingListController', function () {

    beforeEach(module('readingListApp'));

    it('should create a reading list with 2 items to read', inject(function ($controller, $httpBackend) {
 
        $httpBackend.when('GET','/api/readingList').respond([{ "Id": "ItemToReads/1", "Uri": "https://www.youtube.com/watch?v=TAVn7s-kO9o", "Description": "Solid Javascript", "Tags": ["Development", "SOLID"] }, { "Id": "ItemToReads/33", "Uri": "https://www.youtube.com/watch?v=TAVn7s-kO9o", "Description": "Solid Javascript", "Tags": ["Development", "Javascript"] }]);

        var scope = {}, ctrl = $controller('readingListController', { $scope: scope });

        $httpBackend.flush();

        expect(scope.items.length).toBe(2);
    }));

    it('should create a reading list with first item to read', inject(function ($controller, $httpBackend) {

        $httpBackend.when('GET', '/api/readingList').respond([{ "Id": "ItemToReads/1", "Uri": "https://www.youtube.com/watch?v=TAVn7s-kO9o", "Description": "Solid Javascript", "Tags": ["Development", "SOLID"] }, { "Id": "ItemToReads/33", "Uri": "https://www.youtube.com/watch?v=TAVn7s-kO9o", "Description": "Solid Javascript", "Tags": ["Development", "Javascript"] }]);

        var scope = {}, ctrl = $controller('readingListController', { $scope: scope });

        $httpBackend.flush();

        expect(scope.items[0].Id).toBe("ItemToReads/1");
    }));
});