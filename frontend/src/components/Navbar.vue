<template>
    <b-navbar toggleable="lg" type="dark" variant="dark">
        <b-navbar-brand to="/">
            <img src="@/assets/logo_dark.png">
        </b-navbar-brand>
        <b-navbar-toggle target="nav-collapse"></b-navbar-toggle>
        <b-collapse id="nav-collapse" is-nav>
            <b-navbar-nav>

                <!-- Administrator tools -->
                <b-nav-item-dropdown right text="Administrator Manager" v-if="User.role === 'Admin'">
                    <b-dropdown-item to="usermanager"><b-icon-people-fill /> Users</b-dropdown-item>
                    <b-dropdown-item to="register"><b-icon-person-fill /> New user</b-dropdown-item>
                    <b-dropdown-divider></b-dropdown-divider>
                    <b-dropdown-item href="#"><b-icon-shield-lock-fill /> Policy</b-dropdown-item>
                    <b-dropdown-item href="#"><b-icon-shield /> New Policy</b-dropdown-item>
                </b-nav-item-dropdown>
            </b-navbar-nav>

            <!-- Right aligned nav items -->
            <b-navbar-nav class="ml-auto">
                <b-nav-item-dropdown right>
                    <!-- Using 'button-content' slot -->
                    <template v-slot:button-content>
                        <b-icon-people-circle /> {{User.firstName}} {{User.lastName}}
                    </template>
                    <b-dropdown-item href="#">
                        <b-icon-person-lines-fill /> Account settings
                    </b-dropdown-item>
                    <b-dropdown-item to="login">
                        <b-icon-lock /> Sign Out
                    </b-dropdown-item>
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