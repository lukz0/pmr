<template>
    <div>
        <b-container fluid class="bv-example-row">
            <b-row>
                <b-col>
                    <b-row>
                        <b-col cols="8" sm="4"><Sidebar></Sidebar></b-col>
                        <b-col cols="4" sm="8"><router-view></router-view></b-col>
                    </b-row>
                </b-col>
            </b-row>
        </b-container>
    </div>
</template>

<script>
    import Sidebar from "./Sidebar";
    export default {
        name: "UserManager",
        components:{ Sidebar }
    }
</script>

<style scoped>

</style>