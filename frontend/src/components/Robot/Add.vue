<template>
    <b-jumbotron class="mt-4">
        <h1>Add new robot</h1>
        <div>
            <b-form @submit.prevent="handleSubmit">
                <b-form-group label="Hostname" label-for="Robotname">
                    <b-form-input
                            id="Robotname"
                            v-model=robot.hostname
                            type="text"
                            required
                            placeholder="Enter a robot name" />
                </b-form-group>

                <b-form-group
                        label="ip address or domain name"
                        label-for="Address"
                        :description=description(robot.basePath)>
                    <b-form-input
                            id="Address"
                            v-model=robot.basePath
                            required
                            placeholder="Enter robot address" />
                </b-form-group>

                <b-form-group label="UserToken" label-for="User-token">
                    <b-form-input id="User-token" placeholder="Token" />
                </b-form-group>

                <b-button type="submit" variant="primary" id="Add-robot-submit">Connect</b-button>
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