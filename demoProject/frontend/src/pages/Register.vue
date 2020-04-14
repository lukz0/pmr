<template>
    <div>
        <div><h1>Register a new profile </h1></div>
        <div class="form">
            <TextField :label="'First name'" v-model="form.firstName"/>
            <TextField :label="'Last name'" v-model="form.lastname"/>
            <TextField :label="'Username'" v-model="form.username"/>
            <TextField :label="'Password'" v-model="form.password"/>
            <a-sbutton @click="createProfile({api: $api, form})">Submit</a-sbutton>
        </div>

        <div class="list-profiles">
            <div v-if="profiles.length!==0">
                <h3>Registered users</h3>
                <p v-for="p in profiles" :key="p.id">{{p.firstName}}</p>
            </div>

        </div>
    </div>
</template>



<script>
    import TextField from "../components/generic/TextField";
    import ASbutton from "../components/generic/SubmitButton";
    import {mapActions, mapState} from "vuex";

    export default {
        name: 'Register',
        data() {
            return {
                form: {
                    firstName: "",
                    lastname: "",
                    username: "",
                    password: ""
                }
            }
        },
        components: {
            ASbutton,
            TextField
        },
        methods:{
            ...mapActions('profiles',{
                createProfile: 'CREATE_PROFILE'
            })
        },
        computed: {
            ...mapState('profiles',{
                profiles: state => state.profiles
            })
        }
    }
</script>

<style lang="stylus" scoped>
    .list-profiles
        margin-left 30%
        float left
        font-size: 15px;
        color: #000;
        display: block;
    .form
        float left

</style>