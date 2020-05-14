<template>
    <b-jumbotron class="mt-4">
        <h1>Add new robot</h1>
        <div>
            <b-form @submit.prevent="handleSubmit">
                <b-form-group label="Hostname" label-for="input-1">
                    <b-form-input
                            id="label-1"
                            v-model=robot.hostname
                            type="text"
                            required
                            placeholder="Enter a robot name" />
                </b-form-group>

                <b-form-group
                        label="IP-address"
                        label-for="input-2"
                        :description=description(robot.basePath)>
                    <b-form-input
                            id="input-2"
                            v-model=robot.basePath
                            required
                            placeholder="Enter robot API-address" />
                </b-form-group>

                <b-form-group label="UserToken" label-for="input-3">
                    <b-form-input id="input-3" placeholder="Token" />
                </b-form-group>

                <b-button type="submit" variant="primary">Connect</b-button>
            </b-form>
        </div>
    </b-jumbotron>
</template>

<script>
    import { mapActions } from 'vuex'

    export default {
        data() {
            return {
                description: (ip)=> `Api base path will be http://${ip}/api/v2.0.0`,
                robot: {
                    guId: null,
                    basePath: '',
                    username: "admin",
                    password: "1q2w3e4R",
                    hostname: '',
                    isOnline: false
                },
                submitted: false
            }
        },
        methods: {
            ...mapActions('robots', ['add']),
            handleSubmit() {
                this.submitted = true;
                this.$validator.validate().then(valid => {
                    if (valid) {
                        // TODO - link under is for testing purposes only
                        this.robot.basePath = `http://${this.robot.basePath}/api/v2.0.0`;
                        this.add(this.robot);
                    }
                });
            }
        }
    }
</script>