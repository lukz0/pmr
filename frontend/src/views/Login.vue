<template>
<!-- Outer Row -->
    <div class="container">
        <div class="row justify-content-center">
            <div class="col-xl-5 col-lg-12 col-md-9">
                <div class="card o-hidden border-0 shadow-lg my-5">
                    <div class="card-body p-0">
                        <!-- Nested Row within Card Body -->
                        <div class="p-5">
                            <div class="text-center">
                                <h1 class="h4 text-gray-900 mb-4">Welcome Back!</h1>
                            </div>
                            <form class="user" @submit.prevent="handleSubmit">
                                <div class="form-group">
                                    <input type="text"  v-model="username" class="form-control form-control-user" :class="{ 'is-invalid': submitted && !password }" id="InputUsername" placeholder="Enter Username..." />
                                    <div v-show="submitted && !username" class="invalid-feedback">Username is required</div>
                                </div>
                                <div class="form-group">
                                    <input type="password" v-model="password" class="form-control form-control-user" :class="{ 'is-invalid': submitted && !password }" id="InputPassword" placeholder="Password">
                                    <div v-show="submitted && !password" class="invalid-feedback">Password is required</div>
                                </div>
                                <div class="form-group">
                                    <div class="custom-control custom-checkbox small">
                                        <input type="checkbox" class="custom-control-input" id="customCheck">
                                        <label class="custom-control-label" for="customCheck">Remember Me</label>
                                    </div>
                                </div>
                                <button class="btn btn-primary btn-user btn-block" :disabled="status.loggingIn">
                                    Login
                                </button>
                            </form>
                            <hr>
                            <div class="text-center">
                                <a class="small" href="forgot-password.html">Forgot Password?</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>



        </div>
    </div>

</template>

<script>
    import { mapState, mapActions } from 'vuex'
    export default {
        data () {
            return {
                username: '',
                password: '',
                submitted: false
            }
        },
        computed: {
            ...mapState('account', ['status'])
        },
        created () {
            // reset login status
            this.logout();
        },
        methods: {
            ...mapActions('account', ['login', 'logout']),
            handleSubmit () {
                this.submitted = true;
                const { username, password } = this;
                if (username && password) {
                    this.login({ username, password })
                }
            }
        }
    };
</script>

<style scoped>

</style>