/// <reference types="cypress" />

const baseurl = 'https://localhost:5001/';
const user = {};

context('Mirdash', () => {
    beforeEach(() => {
        cy.visit(baseurl);
    });

    // Try to login with invalid username
    it('Submit invalid username and password', () => {
        cy.visit(`${baseurl}login`);

        cy.location('pathname').should('eq', '/login');

        cy.get('#Username').type('null');
        cy.get('#Password').type('null');
        cy.get('#Submit').click();

        cy.location('pathname').should('eq', '/login');
        cy.get('#Alert')
            .should('be.visible')
            .should('contain.text', 'incorrect');
    });

    // Try to login as administrator
    it('Login as admin', () => {
        cy.visit(`${baseurl}login`)
            .location('pathname').should('eq', '/login');
        // Username must be all lowercase
        cy.get('#Username').type('admin');
        cy.get('#Password').type('Password1.');
        cy.get('#Submit').click();

        cy.location('pathname').should('eq', '/');

        cy.get('#Userdropdown>a').click();
        cy.get('#Signout').click()
            .location('pathname').should('eq', '/login');
    });

    it('register new user', () => {
        cy.visit(`${baseurl}login`)
            .location('pathname').should('eq', '/login');

        cy.get('#Username').type('admin');
        cy.get('#Password').type('Password1.');
        cy.get('#Submit').click();

        cy.get('#Userdropdown>a').click();
        cy.get('#Usermanager').click();
        cy.location('pathname').should('eq', '/usermanager');
        cy.get('#UsermanagerRegister').click()
            .location('pathname').should('eq', '/usermanager/register');

        user.username = Math.random().toString(36).substring(2);

        cy.get('#Firstname').type('John');
        cy.get('#Lastname').type('Smith');
        cy.get('#Username').type(user.username);
        cy.get('#Password').type('Password1.');
        cy.get('#Submit').click();

        cy.get('#Alert')
            .should('be.visible')
            .should('contain', 'Registration successful');

        cy.get('#UsermanagerUsers').click()
            .location('pathname').should('eq', '/usermanager');

        cy.get('#UsermanagerUserlist')
            .should('contain', user.username);

        cy.get('#Userdropdown>a').click();
        cy.get('#Signout').click()
            .location('pathname').should('eq', '/login');
    });

    it('login new user', () => {
        cy.visit(`${baseurl}login`)
            .location('pathname').should('eq', '/login');
        cy.get('#Username').type(user.username);
        cy.get('#Password').type('Password1.');
        cy.get('#Submit').click()
            .location('pathname').should('eq', '/');

        cy.get('#Userdropdown>a').click();
        cy.get('#Signout').click()
            .location('pathname').should('eq', '/login');
    });

    it('delete new user', () => {
        cy.visit(`${baseurl}login`)
            .location('pathname').should('eq', '/login');
        cy.get('#Username').type('Admin');
        cy.get('#Password').type('Password1.');
        cy.get('#Submit').click();

        cy.get('#Userdropdown>a').click();
        cy.get('#Usermanager').click();
        cy.location('pathname').should('eq', '/usermanager');

        cy.get(`#UsermanagerDelete${user.username}`).click();
        // TODO: test if the button dissapeared

        cy.get('#Userdropdown>a').click();
        cy.get('#Signout').click()
            .location('pathname').should('eq', '/login');
    });

    it('user was deleted', () => {
        cy.visit(`${baseurl}login`);

        cy.location('pathname').should('eq', '/login');

        cy.get('#Username').type('null');
        cy.get('#Password').type('null');
        cy.get('#Submit').click();

        cy.location('pathname').should('eq', '/login');
        cy.get('#Alert')
            .should('be.visible')
            .should('contain.text', 'incorrect');
    });
});
