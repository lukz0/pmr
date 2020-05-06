<template>
    <b-jumbotron>
        <h1>Add new robot</h1>
        <div>
            <b-form @submit="onSubmit">
                <b-form-group label="Hostname" label-for="input-1">
                    <b-form-input
                            id="label-1"
                            v-model="hostname"
                            type="text"
                            required
                            placeholder="Enter a robot name" />
                </b-form-group>

                <b-form-group
                        label="IP-address"
                        label-for="input-2"
                        v-model="api"
                        description="Example of basepath will be http://192.168.168/api/v2.0.0">
                    <b-form-input
                            id="input-2"
                            v-model="ip_address"
                            required
                            placeholder="Enter robot API-address" />
                </b-form-group>

                <b-form-group label="UserToken" label-for="input-3">
                    <b-form-input id="input-3" required placeholder="Token" />
                </b-form-group>

                <b-button type="submit" variant="primary">Connect</b-button>
            </b-form>
        </div>
    </b-jumbotron>
</template>

<script>
    export default {
        name: "add",
        data() {
            return {
                hostname: '',
                ip_address: ''
            }
        },
        methods: {
            onSubmit(evt) {
                evt.preventDefault()
                alert(JSON.stringify(this.form))
            }
        }
    }
</script>