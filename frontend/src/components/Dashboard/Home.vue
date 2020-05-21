<template>
    <b-jumbotron class="mt-4">
        <b-card-group deck>
            <b-card bg-variant="primary" text-variant="white" class="text-center sm">
                <template v-slot:header>
                    <h2><b-icon-outlet/> Robots</h2>
                </template>
                <b-card-text><h1>3</h1></b-card-text>
            </b-card>
            <b-card bg-variant="secondary" text-variant="white" class="text-center">
                <template v-slot:header>
                    <h2><b-icon-clipboard/> Missions</h2>
                </template>
                <b-card-text><h1>3</h1></b-card-text>
            </b-card>
            <b-card bg-variant="success" text-variant="white" class="text-center">
                <template v-slot:header>
                    <h2>
                        <b-icon-people/>
                        Users
                    </h2>
                </template>
                <b-card-text><h1>32</h1></b-card-text>
            </b-card>
        </b-card-group>
        <Status class="float-right"></Status>
        <BarStatus class="float-right"></BarStatus>
    </b-jumbotron>
</template>

<script>
    import Status from "./Status";
    import BarStatus from "./BarStatus";

    export default {
        name: "Home",
        components: {BarStatus, Status}
    }
</script>

<style scoped>

</style>