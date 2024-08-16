<script setup>
defineProps({
    modalId: { type: String, required: true, default: 'modalWrapper' },
    modalName: { type: String, required: true, default: 'Create Recipe' },
    modalSize: { type: String, required: true, default: 'lg' }
})
</script>


<template>
    <div class="modal fade" :id="modalId" tabindex="-1" :aria-labelledby="`${modalId}Label`" aria-hidden="true">
        <div :class="`modal-dialog modal-${modalSize}`">
            <div class="modal-content">
                <div class="modal-header bg-success">
                    <h1 class="modal-title fs-3 text-light" :id="`${modalId}Label`">{{ modalName }}</h1>
                </div>
                <div class="modal-body">
                    <slot>
                    </slot>
                </div>
            </div>
        </div>
    </div>
</template>


<style lang="scss" scoped></style>