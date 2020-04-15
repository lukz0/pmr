/// <reference types="cypress" />

const baseurl = 'http://localhost:5000/';
const user = {};

context('Mirdash', () => {
    beforeEach(() => {
        cy.visit(baseurl);
    });

    it('submit invalid user and pass', () => {
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

    it('register', () => {
        user.username = Math.random().toString(36).substring(2);
        cy.visit(`${baseurl}register`)
            .location('pathname').should('eq', '/register');
        
        cy.get('#Firstname').type('John');
        cy.get('#Lastname').type('Smith');
        cy.get('#Username').type(user.username);
        cy.get('#Password').type('Password1.');
        cy.get('#Submit').click();

        cy.get('#Alert')
            .should('be.visible')
            .should('contain.text', 'Registration successful');
    });

    it('login', () => {
        cy.visit(`${baseurl}login`)
            .location('pathname').should('eq', '/login');

        cy.get('#Username').type(user.username);
        cy.get('#Password').type('Password1.');
        cy.get('#Submit').click();

        cy.location('pathname').should('eq', '/');
    })
});