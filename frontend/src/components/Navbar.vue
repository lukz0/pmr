<template>
    <b-navbar toggleable="lg" type="dark" variant="dark">
        <b-navbar-brand href="#">MiRDash</b-navbar-brand>
        <b-navbar-toggle target="nav-collapse"></b-navbar-toggle>
        <b-collapse id="nav-collapse" is-nav>
            <b-navbar-nav>
                <b-nav-item to="/register">
                    <b-icon-people-fill/> Manage users</b-nav-item>
                <b-nav-item href="#" disabled>Settings</b-nav-item>
            </b-navbar-nav>

            <!-- Right aligned nav items -->
            <b-navbar-nav class="ml-auto">
                <b-nav-form>
                    <b-form-input size="sm" class="mr-sm-2" placeholder="Search"></b-form-input>
                    <b-button size="sm" class="my-2 my-sm-0" type="submit">Search</b-button>
                </b-nav-form>

                <b-nav-item-dropdown right>
                    <!-- Using 'button-content' slot -->
                    <template v-slot:button-content>
                        <b-icon-people-circle /> {{User.firstName}}
                    </template>
                    <b-dropdown-item href="#">
                        <b-icon-person-lines-fill />
                        Account settings</b-dropdown-item>
                    <b-dropdown-item to="login">
                        <b-icon-lock /> Sign Out</b-dropdown-item>
                </b-nav-item-dropdown>
            </b-navbar-nav>
        </b-collapse>
    </b-navbar>
</template>
<script>
    export default {
        name: "Navbar",
        props: ['User'],
    }
</script>

<style scoped>

</style>