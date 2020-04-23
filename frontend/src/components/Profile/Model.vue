<template>
    <b-modal id="modal-1" title="BootstrapVue">
        <p class="my-4">Hello from modal!</p>
    </b-modal>
</template>

<script>
    export default {
        name: 'Modal',
        props: {
            value: {
                required: true
            }
        },
        methods: {
            close() {
                this.$emit("input", !this.value);
            }
        }
    }
</script>

<style scoped>
    .modal {
        background-color: rgba(0, 0, 0, 0.7);
    }
</style>