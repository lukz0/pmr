<template>
    <b-jumbotron class="mt-4">
        <b-card-group deck>
            <b-card bg-variant="primary" text-variant="white" class="text-center sm">
                <template v-slot:header>
                    <h4>
                        <b-icon-outlet/>
                        Robots
                    </h4>
                </template>
                <b-card-text><h2>3</h2></b-card-text>
            </b-card>
            <b-card bg-variant="secondary" text-variant="white" class="text-center">
                <template v-slot:header>
                    <h4>
                        <b-icon-clipboard/>
                        Missions
                    </h4>
                </template>
                <b-card-text><h2>3</h2></b-card-text>
            </b-card>
            <b-card bg-variant="success" text-variant="white" class="text-center">
                <template v-slot:header>
                    <h4>
                        <b-icon-people/>
                        Users
                    </h4>
                </template>
                <b-card-text><h2>32</h2></b-card-text>
            </b-card>
        </b-card-group>
        <Status class="float-right w-25"></Status>
        <BarStatus class="float-right w-25"></BarStatus>
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