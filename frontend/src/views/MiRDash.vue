<template>
    <div>
        <b-container fluid class="bv-example-row">
            <b-row>
                <b-col>
                    <Navbar></Navbar>
                    <b-row>
                        <b-col><router-view></router-view></b-col>
                    </b-row>
                </b-col>
            </b-row>
        </b-container>
    </div>
</template>

<script>
    import Navbar from "../components/Navbar";
    export default {
        name: "MiRDash",
        components: {Navbar}
    }
</script>

<style scoped>

</style>