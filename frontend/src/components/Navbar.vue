<template>
    <b-navbar toggleable="lg" type="dark" variant="dark">
        <b-navbar-brand to="/">
            <img src="@/assets/logo_dark.png">
        </b-navbar-brand>
        <b-navbar-toggle target="nav-collapse"></b-navbar-toggle>
        <b-collapse id="nav-collapse" is-nav>

            <!-- Right aligned nav items -->
            <b-navbar-nav class="ml-auto">
                <b-nav-item class="mr-2" @click="now"><b>{{time}}</b></b-nav-item>
                <b-nav-item-dropdown right>
                    <!-- Using 'button-content' slot -->
                    <template v-slot:button-content>
                        <!-- test-userdropdown id is used in test -->
                        <b-icon-people-circle id="test-userdropdown" /> {{profile.firstName}} {{profile.lastName}}
                    </template>
                    <b-dropdown-item to="profile">
                        <b-icon-person-lines-fill /> Account settings
                    </b-dropdown-item>
                    <b-dropdown-item to="login">
                        <b-icon-lock id="test-logout"/> Sign Out
                    </b-dropdown-item>
                </b-nav-item-dropdown>
            </b-navbar-nav>
        </b-collapse>
    </b-navbar>
</template>
<script>
    import {mapState} from "vuex";

    export default {
        name: "Navbar",
        data() {
            return {
                time: new Date()
            }
        },
        computed: {
            ...mapState({
                profile: state => state.account.user
            })
        },
        methods: {
            now: function () {
                this.time = new Date()
            }
        }
    }
</script>