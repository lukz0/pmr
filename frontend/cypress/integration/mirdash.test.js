/// <reference types="cypress" />

const baseurl = 'http://localhost:5000/';

context('Mirdash', () => {
    beforeEach(() => {
        cy.visit(baseurl);
    });

    it('submit invalid user and pass', () => {
        cy.visit(`${baseurl}login`);

        cy.location('pathname').should('eq', '/login');

        cy.get('input#Username').type('null');
        cy.get('input#Password').type('null');
        cy.get('button[type="submit"]').click();

        cy.location('pathname').should('eq', '/login');
        cy.get('.alert')
            .should('be.visible')
            .should('contain.text', 'incorrect');
    });
})