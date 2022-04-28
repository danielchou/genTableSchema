<template>
  <q-dialog :model-value="modelValue" no-backdrop-dismiss no-esc-dismiss no-shake full-width @keydown.esc="onCancelByEsc">
    <q-card class="outer-card" :class="{ 'is-update': !isCreate }">
      <q-form @submit="onSubmitForm" class="q-gutter-md">
        <q-card-section class="card-title">
          <div class="text-h6">{{ isCreate ? '新增' : '編輯' }}</div>
        </q-card-section>

        <q-separator />

        <q-card-section style="max-height: 50vh" class="scroll">
$pt_EditForm_q_input
        </q-card-section>

        <q-separator />

        <q-card-actions align="right">
          <div class="text-subtitle2 text-grey-5" v-show="!isCreate">
            <div class="row">
              <span>建立日期：</span>
              <span>{{ formData.createDT }}&nbsp;</span>
              <span>{{ formData.creatorName }}&nbsp;</span>
              <span>{{ formData.creator }}</span>
            </div>
            <div class="row">
              <span>更新日期：</span>
              <span>{{ formData.updateDT }}&nbsp;</span>
              <span>{{ formData.updatorName }}&nbsp;</span>
              <span>{{ formData.updator }}</span>
            </div>
          </div>
          <q-space />
          <q-btn class="col-2 shadow-1" flat label="取消" text-color="secondary" @click="onCancel" />
          <q-btn class="col-2" label="儲存" type="submit" color="secondary" />
        </q-card-actions>
      </q-form>
    </q-card>
  </q-dialog>
</template>

<script>
import { ref, toRefs, watch } from 'vue';
import { useQuasar } from 'quasar';
import { debounce } from 'lodash';
import { useRequiredInput, useMaxLength } from '@composables/use-validations';
import { useCombineFields, useDatetime } from '@composables/use-common';
import { useConfirmDialog } from '@composables/use-dialog';

export default {
  name: 'EditForm',

  props: {
    modelValue: Boolean, // 相當於 vue2 的 value
    isCreate: Boolean,
    fetchItemForEdit: Object,
  },

  emits: ['submit-form', 'update:model-value'],

  setup(props, { emit }) {
    const { modelValue, fetchItemForEdit, isCreate } = toRefs(props);
    const valueChangeFlag = ref(false);
    const $$q = useQuasar();

    const formData = ref({
$pt_editForm_formData
      updateDT: '',
      updator: '',
      updatorName: '',
      createDT: '',
      creator: '',
      creatorName: '',
    });

    watch(
      formData,
      () => {
        valueChangeFlag.value = true;
      },
      {
        deep: true,
      }
    );
    /**
     * 資料 Submit 時從 emit 資料送上一層應該會比較單純
     * 不應存入 Store 裡
     * 除非是有步驟的情況需 keep 住 data
     */
    const onSubmitForm = () => {
      emit('submit-form', formData.value);
    };

    const onCancelByEsc = debounce(() => {
      onCancel();
    }, 200);

    let closeDialogOpened = false;
    const onCancel = () => {
      if (valueChangeFlag.value) {
        closeDialogOpened = true;
        closeDialogOpened &&
          useConfirmDialog($$q, { message: '請確認是否要取消，所有未儲存的異動將會消失。' })
            .onOk(() => {
              emit('update:model-value', false);
            })
            .onDismiss(() => {
              closeDialogOpened = false;
            });
        return;
      }
      emit('update:model-value', false);
    };

    /**
     * 當拿到欲編輯的資料
     * 偵測到視窗打開時就自動將資料塞入進 formData
     */
    if (modelValue.value && !isCreate.value) {
      fetchItemForEdit.value.updateDT = useDatetime(fetchItemForEdit.value.updateDT);
      fetchItemForEdit.value.createDT = useDatetime(fetchItemForEdit.value.createDT);
      formData.value = useCombineFields(formData.value, fetchItemForEdit.value);
      valueChangeFlag.value = false;
    }

    return {
      formData,
      valueChangeFlag,
      onCancelByEsc,
      onCancel,
      onSubmitForm,
      useRequiredInput,
      useMaxLength,
    };
  },
};
</script>

<style lang="scss" scoped>
@import '@styles/quasar.variables.scss';

.outer-card {
  max-width: 60vw !important;
}

.is-update {
  &.outer-card {
    background: rgba($$color: $$esungreen3, $$alpha: 1);
  }
  .card-title {
    background: rgba($$color: $$esungreen, $$alpha: 1);
    color: white;
  }
}
</style>
