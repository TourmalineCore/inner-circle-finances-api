Feature: Test Flow

    Background:

    Scenario: Access denied

        # enter invalid login
        Given url 'https://innercircle.tourmalinecore.com/api'
        Given path 'auth/login'
        And header Content-Type = 'application/json'
        And request { login: '12', password: karate.properties['DEV_KARATE_PASSWORD'] }
        When method POST
        Then status 200

        # enter invalid password
        Given path 'auth/login'
        And header Content-Type = 'application/json'
        And request { login: karate.properties['DEV_KARATE_LOGIN'], password: '12' }
        When method POST
        Then status 401

        # enter invalid creds
        Given path 'auth/login'
        And header Content-Type = 'application/json'
        And request { login: '12', password: '12' }
        When method POST
        Then status 401


    Scenario: Test flow

        # authentication
        Given url 'https://innercircle.tourmalinecore.com/api'
        Given path 'auth/login'
        And header Content-Type = 'application/json'
        And request { login: karate.properties['DEV_KARATE_LOGIN'], password: karate.properties['DEV_KARATE_PASSWORD'] }
        When method POST
        Then status 200
        And match response == { 'accessToken': { 'value': '#string', 'expiresInUtc': '#string' }, 'refreshToken': { 'value': '#string', 'expiresInUtc': '#string' } }
        * def accessToken = karate.toMap(response.accessToken.value)

        # create compensation
        Given path 'salary/compensations/create'
        And header Content-Type = 'application/json'
        And header Authorization = 'Bearer ' + accessToken
        And request { 'compensations': [ { 'typeId': 1, 'comment': 'test - I bought milk', 'amount': 760 }, { 'typeId': 2, 'comment': 'test - I bought this', 'amount': 2760.45 } ], 'dateCompensation': '2023-12-01T00:00:00Z', "employeeId": 2 }
        When method POST
        Then status 200

        # get all personal compensations
        Given path 'salary/compensations/all'
        And header Content-Type = 'application/json'
        And header Authorization = 'Bearer ' + accessToken
        When method GET
        Then status 200

        # get types
        Given path 'salary/compensations/types'
        And header Content-Type = 'application/json'
        And header Authorization = 'Bearer ' + accessToken
        When method GET
        Then status 200
        And match response contains [ { "typeId": 1, "label": "English" }, { "typeId": 2, "label": "German" }, { "typeId": 3, "label": "Swimming" }, { "typeId": 4, "label": "Water" }, { "typeId": 5, "label": "Coworking" }, { "typeId": 6, "label": "Massage" }, { "typeId": 7, "label": "Products" }, { "typeId": 8, "label": "Consumables" }, { "typeId": 9, "label": "Periphery" }, { "typeId": 10, "label": "Business trip" }, { "typeId": 11, "label": "Psychotherapy" }, { "typeId": 12, "label": "Other" } ]

        # get all admin compensations
        Given path 'salary/compensations/admin/all'
        And header Content-Type = 'application/json'
        And header Authorization = 'Bearer ' + accessToken
        When method GET
        Then status 200

        # update compensation status
        Given path 'salary/compensations/admin/update'
        And header Content-Type = 'application/json'
        And header Authorization = 'Bearer ' + accessToken
        And request [ 55, 56 ]
        When method PUT
        Then status 200