/// <reference types="cypress" />

const baseurl = 'https://localhost:5001/';
const user = {};

context('Mirdash', () => {
    beforeEach(() => {
        cy.visit(baseurl);
        // load in some users from fixture/users.json
        cy.fixture('users.json').as('users');
    });

    let login = function loginUser(username, password) {
      cy.visit(`${baseurl}login`)
          .location('pathname').should('eq', '/login');

      cy.get('#Username').type(username);
      cy.get('#Password').type(password);
      cy.get('#Submit').click();
    }

    let logout = function logoutUser() {
      cy.get('#test-userdropdown').click();
      cy.get('#test-logout').click()
          .location('pathname').should('eq', '/login');
    }

    // Try to login with invalid username and password
    it('Submit invalid username and password', () => {
        cy.visit(`${baseurl}login`);

        cy.location('pathname').should('eq', '/login');

        // login invalid user
        login('null', 'null');

        cy.location('pathname').should('eq', '/login');
        cy.get('.modal-body')
            .should('be.visible')
            .should('contain.text', ' Username or password is incorrect ');
    });

    // Try to login as administrator then logout
    it('Login as admin', () => {
        cy.visit(`${baseurl}login`)
        .location('pathname').should('eq', '/login');

        // Username must be all lowercase
        login('admin', 'Password1.');

        cy.location('pathname').should('eq', '/');

        logout()
    });

    // Try to Register a user using admin account
    it('Register new user', () => {
        cy.visit(`${baseurl}login`)
            .location('pathname').should('eq', '/login');

        login('admin', 'Password1.');

        cy.get('#test-add-user').click();
        cy.location('pathname').should('eq', '/register');

        user.username = Math.random().toString(36).substring(2);

        cy.get('#Firstname').type('John');
        cy.get('#Lastname').type('Smith');
        cy.get('#Username').type(user.username);
        cy.get('#Password').type('Password1.');
        cy.get('#Submit').click();

        // Will redirect to users page if successful
        cy.get('#test_btn_ok').click()
            .location('pathname').should('eq', '/usermanager');
        //
        // cy.get('#test-all-users').click()
        //     .location('pathname').should('eq', '/usermanager');

        cy.get('#test-users-list')
            .should('contain', user.username);

        logout();
    });

    // Login the registerd user from above
    it('Login new user', () => {

      login(user.username, 'Password1.')

      logout()
    });

    // Login as admin and delete the registed user from above
    it('Delete new user', () => {
        login('admin', 'Password1.')

        cy.get('#test-all-users').click();
        cy.location('pathname').should('eq', '/usermanager');

        cy.get(`#test-delete-user${user.username}`).click();

        logout()
    });

    // Try to login with the delete account
    it('User was deleted', () => {

        login(user.username, 'Password1.')

        cy.get('.modal-body')
            .should('be.visible')
            .should('contain.text', ' Username or password is incorrect ');
    });

    // Register dummy
    it('Register dummy users', function () {

     for(let user of this.users){
       cy.visit(`${baseurl}login`)
           .location('pathname').should('eq', '/login');

       // Sign in as admin
       login('admin', 'Password1.');

       cy.get('#test-add-user').click();
       cy.location('pathname').should('eq', '/register');

       cy.get('#Firstname').type(user.name);
       cy.get('#Lastname').type(user.name);
       cy.get('#Username').type(user.username);
       cy.get('#Password').type('Password1.');

       cy.get('#Submit').click();

       // Will redirect to users page if successful
       cy.get('#test_btn_ok').click()
           .location('pathname').should('eq', '/usermanager');

       cy.get('#test-all-users').click()
           .location('pathname').should('eq', '/usermanager');

      // Confirm that the user was registerd
       cy.get('#test-users-list').should('contain', user.name);

        // Sign logout
       logout();
     }
   })


});
