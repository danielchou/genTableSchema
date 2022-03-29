<template>
  <q-dialog :model-value="modelValue" no-backdrop-dismiss no-esc-dismiss no-shake full-width>
    <q-card class="outer-card" :class="{ 'is-update': !isCreate }">
      <q-form @submit="onSubmitForm" class="q-gutter-md">
        <q-card-section class="card-title">
          <div class="text-h6">{{ isCreate ? '新增' : '編輯' }}</div>
        </q-card-section>

        <q-separator />

        <q-card-section style="max-height: 50vh" class="scroll">
          <div class="row q-col-gutter-md">
            <!-- COPYDANIEL: 一個 q-input 為一組 -->
            <q-input
              class="col"
              type="text"
              v-model.trim="formData.computerName"
              label="電腦名稱"
              lazy-rules
              :rules="[useRequiredInput, (val) => useMaxLength(val, 20)]"
            />
            <q-input
              class="col"
              type="text"
              v-model.trim="formData.computerIp"
              label="IP 位址"
              lazy-rules
              :rules="[useRequiredInput, (val) => useMaxLength(val, 20)]"
            />
          </div>
          <div class="row q-col-gutter-md">
            <q-input
              class="col"
              type="text"
              v-model.trim="formData.extCode"
              label="分機號碼"
              lazy-rules
              :rules="[useRequiredInput, (val) => useMaxLength(val, 20)]"
            />
          </div>
          <div class="row q-col-gutter-md">
            <q-input
              class="col"
              type="text"
              v-model.trim="formData.memo"
              label="備註"
              lazy-rules
              :rules="[useRequiredInput, (val) => useMaxLength(val, 20)]"
            />
          </div>
          <!-- COPYDANIEL: 固定的，免套 -->
          <div class="row q-col-gutter-md" v-show="!isCreate">
            <q-input
              class="col"
              type="text"
              :model-value="formData.updator"
              label="最後修改人員"
              borderless
              disable
              readonly
            />
            <q-input
              class="col"
              type="text"
              :model-value="formData.updateDt"
              label="最後修改時間"
              borderless
              disable
              readonly
            />
          </div>
        </q-card-section>

        <q-separator />

        <q-card-actions align="right">
          <q-toggle
            v-model="formData.isEnable"
            color="primary"
            :label="formData.isEnable ? '啟用' : '停用'"
          />
          <q-space />
          <q-btn flat label="取消" color="primary" @click="onCancel" />
          <q-btn label="儲存" type="submit" color="primary" />
        </q-card-actions>
      </q-form>
    </q-card>
  </q-dialog>
</template>

<script>
import { ref, toRefs, watch } from 'vue';
import { useQuasar } from 'quasar';
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
    const $q = useQuasar();

    // COPYDANIEL: Table Schema
    const formData = ref({
      seqNo: 0,
      extCode: '',
      computerName: '',
      computerIp: '',
      memo: '',
      isEnable: true,
      updateDt: '',
      updator: '',
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

    const onCancel = () => {
      if (valueChangeFlag.value) {
        useConfirmDialog($q, { message: '請確認是否要取消，所有未儲存的異動將會消失。' }).onOk(
          () => {
            emit('update:model-value', false);
          }
        );
        return;
      }
      emit('update:model-value', false);
    };

    /**
     * 當拿到欲編輯的資料
     * 偵測到視窗打開時就自動將資料塞入進 formData
     */
    if (modelValue.value && !isCreate.value) {
      fetchItemForEdit.value.updateDt = useDatetime(fetchItemForEdit.value.updateDt);
      formData.value = useCombineFields(formData.value, fetchItemForEdit.value);
      valueChangeFlag.value = false;
    }

    return {
      formData,
      valueChangeFlag,
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
    background: rgba($color: $esungreen3, $alpha: 1);
  }
  .card-title {
    background: rgba($color: $esungreen, $alpha: 1);
    color: white;
  }
}
</style>
