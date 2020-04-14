<template>
    <div>
        <div v-if="profile">
            <h1>Welcome to {{profile.firstName}}'s profile</h1>
            <table>
                <tr><td>Fist name: </td><td>{{profile.firstName}}</td></tr>
                <tr><td>Last name: </td><td>{{profile.lastName}}</td></tr>
                <tr><td>Username: </td><td>{{profile.userName}}</td></tr>
                <tr><td>Password: </td><td>{{profile.password}}</td></tr>
            </table>
            <router-link :to="'/profile'">Back</router-link>
        </div>
        <div v-else>
            <h1>Profiles</h1>
            <ul>
                <li v-for="p in profiles" :key="p.id">
                    <router-link
                            :to="`/profile/${p.firstName}`"
                            >
                        {{p.firstName}}
                    </router-link>
                </li>
            </ul>

        </div>

    </div>
</template>

<script>
    import {mapActions, mapGetters, mapState} from "vuex";
    export default {
        name: "Profile",
        watch: {
            profile: {
                immediate: true,
                handler(profile){
                    if (profile !== undefined)
                    this.setPopup("Selected Profile: " + profile.firstName);
                }
            }
        },
        methods: {
            ...mapActions('popup',{
                setPopup: 'DISPLAY_POPUP'
            })
        },
        computed: {
            ...mapGetters('profiles',{
                getProfile: 'GET_PROFILE'
            }),
            profile(){
                return this.getProfile(this.$route.params.name)
            },
            ...mapState('profiles',{
                profiles: state => state.profiles
            })
        }
    };
</script>

<style lang="stylus" scoped>
    h1
        display block
        position center



</style>